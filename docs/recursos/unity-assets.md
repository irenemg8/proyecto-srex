# Inventario de assets Unity

Última comprobación de páginas: 12 de noviembre de 2026. Proyecto de destino: Unity 6000.0.83f1, URP 17.0.4, Meta XR All-in-One 209.0.0, Quest 3.

Los estados «candidato» no implican descarga, licencia adquirida ni prueba en el proyecto. Para la memoria se conserva siempre el enlace de producto, autor/editor, tipo de licencia, versión y fecha de consulta. La licencia aplicable es la que acompaña a la copia adquirida/importada.

| Recurso | Procedencia y licencia | Compatibilidad publicada | Estado y notas |
|---|---|---|---|
| [DOLLHOUSE — FREE — Modular Low Poly Interiors: Furniture & Room Kit](https://assetstore.unity.com/packages/3d/props/interior/dollhouse-free-modular-low-poly-interiors-furniture-room-kit-411314) | ForgeWorks, Unity Asset Store. Gratis; Standard Unity Asset Store EULA; tipo Extension Asset. | La ficha lista Unity 6000.4.2f1 como compatible con Built-in, URP y HDRP. | `usados.txt` contiene el enlace y existe `Proyecto/Assets/LowPolyInteriorsFree/`. Ya está en el repo, pero la versión indicada en la ficha es posterior a la del proyecto (6000.0.83f1): compatibilidad efectiva pendiente de revisar dentro de Unity. No atribuirle compatibilidad verificada aún. |
| [Food FREE — Low Poly 3D Models Pack](https://assetstore.unity.com/packages/3d/props/food/food-free-low-poly-3d-models-pack-260726) | ithappy, Unity Asset Store. Gratis; Standard Unity Asset Store EULA; tipo Extension Asset. | La ficha lista Unity 6000.0.28f1 compatible con URP, Built-in y HDRP; incluye archivos para Unity 6000. | `candidato recomendado` para alimentos: la versión publicada coincide con Unity 6.0 y el editor del proyecto es un parche posterior de 6000.0. Probar materiales, tamaño, colliders y rendimiento en Quest antes de adoptarlo. |
| [Low Poly Kitchen Set](https://assetstore.unity.com/packages/3d/props/interior/low-poly-kitchen-set-303227) | HunterCraft Creations, Unity Asset Store. Gratis; Standard Unity Asset Store EULA; tipo Extension Asset. | La ficha lista Unity 2022.3.14 compatible con URP y Built-in. | `candidato alternativo` de tamaño reducido (966.1 KB según la ficha). Unity 6 no aparece explícitamente en su matriz; hacer importación de prueba y revisar shaders/materiales. |
| [Free Kitchen — Cabinets and Equipment](https://assetstore.unity.com/packages/3d/props/interior/free-kitchen-cabinets-and-equipment-245554) | Boxx-Games Assets, Unity Asset Store. Gratis; Standard Unity Asset Store EULA; tipo Extension Asset. | La ficha lista URP para Unity 2021.3.24; indica que el paquete trae un `URP_Material.unitypackage` para convertir materiales. | `candidato de reserva`; 216.1 MB según la ficha. Coste de descarga/importación y conversión mayor; no es la primera opción para un prototipo Quest si basta el pack pequeño. |

## Regla para la memoria

La EULA estándar permite incorporar assets como componentes de un producto; no redistribuir los ficheros fuente como descarga independiente. Aunque la licencia no exija atribución en todos los casos, la memoria del TF debe citar editor, título, URL, fecha de consulta y uso concreto. Consultar la [EULA vigente](https://unity.com/legal/as-terms); si un asset incluye términos propios, estos prevalecen para ese asset.

## Registro de integración

| Fecha | Asset | Ruta en el repo/escena | Resultado en Unity/Quest | Decisión |
|---|---|---|---|---|
| Pendiente | Dollhouse | `Proyecto/Assets/LowPolyInteriorsFree/` | Pendiente de revisar consola, materiales y escena en Unity 6000.0.83f1 | Mantener por estar ya importado; no confirmar compatibilidad hasta probar |
