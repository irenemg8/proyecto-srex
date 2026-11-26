# Decisiones y guía del proyecto VR: fruta a la sartén

> Documento vivo: recoge las decisiones acordadas y el estado de implementación. En la revisión actual se permiten cambios en assets y configuración del proyecto; no se modificarán scripts C# ni preferencias de GPU/Windows sin autorización explícita.

## 1. Idea

La persona usuaria recorre una casa/cocina virtual, agarra piezas de fruta y las lleva hasta una sartén. El objetivo es una experiencia breve, comprensible y demostrable en Meta XR Simulator.

## 2. Requisitos docentes que condicionan el diseño

El enunciado del trabajo final exige una aplicación de **realidad virtual** para Meta Quest 3 (no realidad mixta) e integrar configuración XR, interacción cercana y a distancia, locomoción, sonido 3D e interfaz espacial. La memoria final deberá documentar qué se hizo y cómo, con capturas, recursos y reparto del trabajo.

- **Locomoción:** implementar movimiento continuo *Slide* y teletransporte, conmutables durante la ejecución; incluir al menos una opción de confort. No se debe escoger uno y omitir el otro.
- **Interacción:** al menos un agarre cercano, una interacción a distancia, respuesta programada en C# y compatibilidad con mandos y manos.
- **Audio:** ambiente 2D, dos emisores espaciales con atenuación, Audio Mixer/acústica de sala y un sonido ligado a una acción.
- **UI:** Canvas espacial con prefabs de Meta UI Set, accionable por rayo o poke, y al menos tres controles funcionales: volumen, locomoción y una función de la temática.

Las prácticas P1–P4 son la guía de implementación de los bloques. P2 trata de RM: **no** añadiremos passthrough ni MRUK, porque contradicen el requisito de RV del trabajo final. De P4 se conserva la estructura técnica de audio/UI, adaptando la función de figuras geométricas a la actividad de la fruta.

## 3. Decisiones de diseño y justificación

### Escenario y recursos

- Aprovechar el kit de interiores low-poly ya presente y la escena `Casa`; usar la cocina como espacio principal. Reutilizar muebles y superficies antes de descargar más recursos.
- Usar fruta como objeto manipulable y una sartén como destino reconocible. Según `usados.txt`, están anotados el kit de interiores, un paquete de fruta y un modelo de sartén; confirmar/importar esos recursos desde Unity y registrar su procedencia en la memoria.
- Mantener escala real (1 unidad de Unity = 1 metro), suelo/superficies con colliders y una jerarquía legible. La fruta necesita collider y Rigidbody para que el agarre y la entrega sean previsibles.
- **Paredes y colisión del usuario:** poner colliders estáticos en paredes (preferiblemente Box Collider; Mesh Collider si la forma lo exige) y suelo. No añadir Rigidbody a las paredes ni al Camera Rig/CenterEyeAnchor. El locomotor del Meta Interaction SDK debe tener su PlayerController/cápsula y Character Controller configurados para colisionar con las capas de suelo y paredes; los colliders por sí solos no frenan la cámara si el desplazamiento no pasa por ese locomotor.

### Movimiento continuo **y** teletransporte

No planteamos “movimiento continuo en vez de teletransporte”: implementaremos ambos porque así lo exige el enunciado y lo enseña P3.

- **Slide** permite recorrer y explorar la cocina sin saltos de posición, y hace que el desplazamiento se sienta continuo.
- **Teleport** evita el flujo óptico sostenido, facilita desplazarse con poco esfuerzo y es una alternativa más confortable para quien no tolere Slide.
- **Propuesta inicial:** arrancar en Teleport por defecto y permitir activar Slide en cualquier momento desde el Toggle de la interfaz. La teoría de locomoción recomienda una opción inicial orientada al confort; P3 muestra Slide como estado inicial, pero el enunciado final no fija cuál debe serlo. En la demostración se enseñarán ambos modos.
- Colocar hotspots en zonas libres y seguras (la práctica propone 10–15); no poner destinos dentro de muebles, sobre la sartén ni en zonas que bloqueen la interacción.

### Medidas para reducir el mareo (túnel y otras)

El *tunneling* reduce la visión periférica durante el movimiento para disminuir el flujo óptico. Es una ayuda, no una garantía de que nadie se maree: la sensibilidad varía entre personas.

1. Activar/configurar el efecto túnel (*SmoothMovementTunneling*) del locomotor para Slide; comprobarlo al desplazarse y ajustar su nivel/intensidad.
2. Preferir **Snap Turn** frente a giro suave continuo.
3. Mantener velocidad y aceleración moderadas; evitar aceleraciones bruscas, cámara forzada, balanceos y movimiento automático de la cabeza.
4. Mantener Teleport disponible y visible como alternativa inmediata; permitir parar soltando el joystick.
5. Empezar las pruebas con sesiones cortas, preguntar si hay incomodidad y detenerse/volver a Teleport si aparece. No presentar estas medidas como prevención total.

### Interacción y objetivo

- Usar **Grab** para coger la fruta cercana con mando o mano; al soltarla sobre la sartén, detectar la entrega (por ejemplo, con un trigger bien dimensionado) y dar feedback visual/sonoro.
- Cubrir la interacción a distancia mediante **Ray** en la UI espacial. Si resulta viable, permitir también seleccionar/traer fruta con Distance Grab, pero no sacrificar la interacción cercana.
- La sartén debe ser un destino claro y estable; evitar que la fruta atraviese la encimera o rebote fuera con colliders y una zona de recepción amplia.
- Función temática propuesta para la UI: botón para reiniciar/reponer la fruta y el objetivo. Los tres controles mínimos serán volumen ambiental, modo Slide/Teleport y reinicio de la tarea.
- Mostrar feedback de selección, agarre, entrega y finalización (visual y sonoro); usar texto/instrucciones breves.

### Sonido

- Ambiente continuo en 2D, separado de los efectos en un Audio Mixer.
- Dos emisores espaciales 3D situados en fuentes visibles de la cocina (por ejemplo, nevera/cocina y sartén), con atenuación por distancia comprobable acercándose y alejándose.
- Reproducir un efecto al completar la entrega de fruta. El control de volumen de la UI regulará el ambiente.
- Configurar la acústica de sala con el modelo *Shoebox* y dimensiones/materiales aproximados de la cocina, conforme a P4; priorizar estabilidad y rendimiento antes que ray tracing acústico.
- Registrar autor, URL y licencia de los clips utilizados. `usados.txt` incluye una atribución de Pixabay para un efecto de sonido; verificar qué archivo corresponde antes de atribuirlo.

### Interfaz espacial

- Usar `EmptyUIBackplateWithCanvas` y los prefabs de Meta UI Set; ubicar el panel aproximadamente a 1,5–2 m, con texto grande, contraste y pocos controles.
- Interacción principal por rayo (cómoda a distancia); poke puede añadirse si funciona bien sin duplicar complejidad.
- Controles conectados a funciones reales: volumen ambiente, Toggle de locomoción y reinicio/reposición de fruta.

## 4. Orden de trabajo propuesto (sin cambios automáticos en Unity)

1. Abrir el proyecto Unity existente y hacer una copia de la escena `Casa` para trabajar sin alterar la original.
2. Comprobar XR/OpenXR, Meta XR All-in-One SDK, Building Block Camera Rig, origen `Floor Level`, escena a escala real y validación del proyecto.
3. Preparar la cocina: suelo/superficies y paredes con colliders; comprobar PlayerController/Character Controller y sus capas; ajustar ubicación inicial y jerarquía.
4. Incorporar sartén y fruta; configurar la fruta como objeto agarrable cercano y probarla antes de añadir lógica.
5. Añadir la interacción a distancia y la lógica C# de entrega/reinicio.
6. Configurar Slide + Teleport, Toggle y confort; probar el túnel, giros y hotspots en Simulator.
7. Añadir ambiente, emisores 3D, Audio Mixer y acústica; conectar el control de volumen y el sonido de entrega.
8. Montar la UI espacial Meta, conectar sus tres controles y revisar distancia/legibilidad.
9. Probar con Simulator cada requisito y grabar el recorrido de demostración; guardar capturas para la memoria.

## 5. Base documental revisada

- `trabajo final SREX.pdf`: requisitos, mínimos técnicos, entregas y evaluación.
- `Practicas_referencia/P1.pdf`: escala, jerarquía, escena, Camera Rig y Floor Level.
- `Practicas_referencia/P2.pdf`: configuración de RM (consultada solo para distinguirla; no aplicarla a este proyecto de RV).
- `Practicas_referencia/P3.pdf`: Grab, Ray, locomoción híbrida, alternancia y prueba en Meta XR Simulator.
- `Practicas_referencia/P4.pdf`: sonido espacial, Audio Mixer, *Shoebox*, Meta UI Set y Toggle de locomoción.
- `Teoria/Tema_3.1_T_cnicas_de_interacci_n.pdf`, `Tema_3.2_Uso_de_controladores.pdf` y `Tema_3.3._Uso_de_tracking_de_manos.pdf`: interacción directa/indirecta, affordances, feedback y entrada multimodal.
- `Teoria/Tema_3.4._Desplazamiento_con_joystick.pdf` y `Tema_3.5._Desplazamiento_con_teletransporte.pdf`: conflicto sensorial, Slide, confort túnel, Snap Turn y Hotspots.
- `Teoria/Tema_4.1._Sonido_3D.pdf` y `Tema_4.2._Interfaces_de_usuario.pdf`: HRTF, atenuación, acústica, controles y ergonomía de UI espacial.
- `Teoria/Tema_2.1._Utilizaci_n_de_librer_as_XR.pdf`, `Tema_2.2_Configuraci_n_para_aplicaci_n_de_realidad_virtual.pdf` y `Tema_2.3_Scripts_en_Unity.pdf`: librerías, OpenXR, Camera Rig y fundamentos C#.
- `usados.txt`: referencias de recursos y atribuciones que habrá que validar al utilizarlos.

## 6. Pendiente de confirmar durante las pruebas

- Que los modelos anotados en `usados.txt` estén importados y sean compatibles con la versión del proyecto.
- Posición/tamaño final de fruta, sartén, panel y hotspots tras probar a escala real.
- Intensidad del túnel, velocidad de Slide y nivel de snap-turn que resulten cómodos y fáciles de demostrar.
- Atribución exacta de cada sonido usado.
