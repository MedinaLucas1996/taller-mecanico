Module Recordatorios
    'regla compartida de los avisos de service: la usan la pantalla Services por vencer y la tarjeta de Inicio

    'cuantos dias hacia adelante mira la lista "vencen pronto"
    Public Const DIAS_PROXIMOS As Integer = 30
    'durante estos dias un aviso enviado deja el service en gris y al final de la lista
    Public Const DIAS_AVISO_RECIENTE As Integer = 15

    Function ConsultaServices() As String
        'devuelve el select de los services que vencen entre @desde y @hasta (fechas)
        '@limite_aviso es la fecha y hora desde la que un aviso cuenta como reciente
        'por cada vehiculo y servicio con intervalo en meses tomo la ultima orden ENTREGADA que lo hizo
        '(linea aprobada): vence en la fecha de entrega mas el intervalo; los km son solo una referencia
        'el mensaje va al titular actual del vehiculo; un vehiculo con una orden abierta ya esta en el taller y no se lista
        'el aviso que se muestra es el ultimo enviado despues de ese service
        'turno es el proximo turno pendiente del vehiculo (la franja en curso incluida), NULL si no tiene
        Return _
            "SELECT v.id_vehiculo, s.id_servicio, c.razon_social AS cliente, c.telefono, v.patente, " &
            "CONCAT(ma.descripcion, ' ', mo.descripcion) AS vehiculo, s.descripcion AS servicio, " &
            "u.fecha_entrega AS ultimo, u.km_ingreso AS ultimo_km, " &
            "CASE WHEN s.intervalo_km IS NULL THEN NULL ELSE u.km_ingreso + s.intervalo_km END AS proximo_km, " &
            "DATE(u.fecha_entrega + INTERVAL s.intervalo_meses MONTH) AS vence, " &
            "CASE WHEN a.fecha_hora IS NULL THEN '' " &
            "ELSE CONCAT('Avisado el ', DATE_FORMAT(a.fecha_hora, '%d/%m/%Y'), ' por ', ua.nombre_completo) END AS aviso, " &
            "CASE WHEN a.fecha_hora >= @limite_aviso THEN 1 ELSE 0 END AS avisado_reciente, " &
            "(SELECT MIN(t.fecha_hora) FROM turno AS t " &
            " WHERE t.id_vehiculo = v.id_vehiculo AND t.estado = 'PENDIENTE' " &
            " AND t.fecha_hora >= NOW() - INTERVAL 1 HOUR) AS turno " &
            "FROM (SELECT o.id_vehiculo, d.id_servicio, o.fecha_entrega, o.km_ingreso, " &
            "      ROW_NUMBER() OVER (PARTITION BY o.id_vehiculo, d.id_servicio " &
            "                         ORDER BY o.fecha_entrega DESC, o.id_orden_trabajo DESC) AS n " &
            "      FROM orden_trabajo AS o " &
            "      JOIN estado_ot AS e ON e.id_estado_ot = o.id_estado_ot " &
            "      JOIN ot_detalle AS d ON d.id_orden_trabajo = o.id_orden_trabajo " &
            "      WHERE e.codigo = 'ENTREGADA' AND d.aprobado = 1 AND o.fecha_entrega IS NOT NULL) AS u " &
            "JOIN servicio AS s ON s.id_servicio = u.id_servicio " &
            "JOIN vehiculo AS v ON v.id_vehiculo = u.id_vehiculo " &
            "JOIN cliente AS c ON c.id_cliente = v.id_cliente " &
            "JOIN modelo AS mo ON mo.id_modelo = v.id_modelo " &
            "JOIN marca AS ma ON ma.id_marca = mo.id_marca " &
            "LEFT JOIN aviso_service AS a ON a.id_aviso_service = " &
            "    (SELECT a2.id_aviso_service FROM aviso_service AS a2 " &
            "     WHERE a2.id_vehiculo = u.id_vehiculo AND a2.id_servicio = u.id_servicio " &
            "     AND a2.fecha_hora >= u.fecha_entrega " &
            "     ORDER BY a2.fecha_hora DESC, a2.id_aviso_service DESC LIMIT 1) " &
            "LEFT JOIN usuario AS ua ON ua.id_usuario = a.id_usuario " &
            "WHERE u.n = 1 AND s.activo = 1 AND s.intervalo_meses IS NOT NULL " &
            "AND v.activo = 1 AND c.activo = 1 " &
            "AND NOT EXISTS (SELECT 1 FROM orden_trabajo AS o2 " &
            "                JOIN estado_ot AS e2 ON e2.id_estado_ot = o2.id_estado_ot " &
            "                WHERE o2.id_vehiculo = v.id_vehiculo AND e2.es_estado_final = 0) " &
            "AND DATE(u.fecha_entrega + INTERVAL s.intervalo_meses MONTH) BETWEEN @desde AND @hasta "
    End Function

End Module
