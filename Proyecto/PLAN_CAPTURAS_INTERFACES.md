# Plan de capturas de pantalla — Sección «Interfaces» (TFG)

**Proyecto:** `Proyecto/Proyecto/Proyecto/` (Editor Unity)  
**Escena fuente:** `Assets/_Proyecto/Scenes/Casa.unity`  
**Scripts implicados:** `Assets/_Proyecto/Scripts/` (8 controladores)  
**Destino de las imágenes:** carpeta de imágenes de la memoria LaTeX (referenciadas con `\captura{...}`)

> **Alcance de este documento:** planificación + justificación de cada captura.  
> Las imágenes deben ser tomadas **tú mismo desde la interfaz de Unity / el juego propio** (Editor o modo Play), no son ilustraciones genéricas ni mockups. Yo no puedo lanzar Unity directamente desde este entorno, así que este plan es la guía ejecutable para que las captures tú.

---

## 0. Requisitos previos (hazlo una sola vez antes de capturar)

1. Abre el proyecto en Unity Editor y carga la escena `Casa.unity`.
2. Asegúrate de que el **XR Rig / cámara XR** está presente y activo (necesario para modo Play y para medir la distancia de lectura).
3. Activa los **Gizmos** si quieres mostrar el origen de los prefabs o una referencia de distancia en la escena (útil para `interfaz_espacial.png`).
4. Configura la **ventana Game** a una resolución representativa (p. ej. 1920×1080) y la **ventana Scene** en una perspectiva que muestre el panel con profundidad (no ortográfica plana).
5. Para las capturas en modo Play (`interfaz_rayo.png`, `interfaz_poke.png`), entra en Play y usa:
   - **Mando:** rayo con botón de disparo (trigger) para selección a distancia; punta del mando para poke.
   - **Mano:** rayo desde la palma/puntero y poke con el dedo índice.

---

## 1. Resumen de las 4 capturas

| # | Archivo | Contexto | Qué debe demostrar | Justificación clave |
|---|---------|----------|--------------------|---------------------|
| 1 | `interfaz_editor.png` | Editor (no Play) | Panel de ajustes en el Editor, tal como viene del proyecto recibido | Documenta autoría/montaje; no es prueba en ejecución |
| 2 | `interfaz_espacial.png` | Editor o Play | Panel en mundo virtual, Canvas World Space, prefabs Meta UI Set, distancia lectura 1,5–2 m | World Space da presencia espacial y distancia física real |
| 3 | `interfaz_rayo.png` | Play mode | Rayo sobre un control + resultado observable (dropdown abierto o slider movido) | Selección a distancia con trigger (no grip); debe verse el efecto |
| 4 | `interfaz_poke.png` | Play mode | Contacto dedo/punta de mando con botón + respuesta del control | Interacción cercana por contacto; debe verse la respuesta, no solo el componente |

---

## 2. Detalle, pasos y justificación por captura

### 2.1 `interfaz_editor.png`

**Qué debe mostrar:**  
El panel de ajustes dentro del **Editor de Unity**, tal como se entregó el proyecto. Deben verse:
- La jerarquía (Hierarchy) con el panel como GameObject y sus prefabs del Meta UI Set (`EmptyUIBackplateWithCanvas` como raíz, y los controles anidados).
- El Inspector del Canvas con **Render Mode = World Space**.
- Los componentes de organización: `HorizontalLayoutGroup`, `VerticalLayoutGroup`, `LayoutElement`.
- Los scripts asignados a cada control (`AmbientVolumeController`, `LocomotionModeController`, `HelpDialogController`, `SpatialEffectsController`, `ProgressRefreshButton`, `ExitDialogController`) con sus referencias vinculadas.
- Textos en **TextMesh Pro** (`TMP_Text`).

**Pasos en Unity:**
1. Queda en modo **Editor** (no pulses Play).
2. Selecciona en la Hierarchy el GameObject raíz del panel (el que tiene `EmptyUIBackplateWithCanvas`).
3. Abre el Inspector y déjalo visible (docked o flotante) para que se lean los componentes.
4. Posiciona la cámara de la ventana Scene para que se vea el panel con algo de profundidad.
5. Haz una captura que incluya: ventana Scene (panel en 3D) + ventana Inspector (componentes y scripts). Si es muy denso, prioriza el Inspector del Canvas + jerarquía, que es lo que el texto pide ("Vista del panel de ajustes en el Editor").

**Justificación de la elección:**  
La memoria dice explícitamente *"Vista del panel de ajustes en el Editor, conservada del proyecto recibido"* y aclara *"No es una prueba de interacción en ejecución"*. Por eso se captura en **Editor**, no en Play mode. Capturar en Play mode contradiría la aclaración y no aportaría información de autoría (prefabs, LayoutGroup, asignación de scripts en el Inspector).

**Por qué esto y no lo otro:**
- **No Play mode:** el requisito la desvincula de una prueba en ejecución; Play mode oculte el Inspector y no mostraría el montaje.
- **No solo la ventana Game:** no mostraría la jerarquía ni el Inspector, que son esenciales para documentar el origen en prefabs y los scripts conectados.
- **No un mockup del Meta UI Set:** no representaría la implementación real del proyecto (prefabs adaptados + scripts propios).

**Criterio de aceptación:** se lee el Canvas en World Space, los prefabs del UI Set en la jerarquía y al menos un script de control asignado con su referencia rellena.

---

### 2.2 `interfaz_espacial.png`

**Qué debe mostrar:**  
El panel situado en el mundo virtual, evidenciando:
- El **Canvas en modo World Space** (componente Canvas, Render Mode = World Space).
- El prefab origen `EmptyUIBackplateWithCanvas` como base del panel y los prefabs anidados del Meta UI Set (slider, toggle, botones).
- La escala real del panel (el backplate usa escala `0.001`, típico de World Space para que 1200×750 px sean ~1.2×0.75 unidades de mundo).
- La **distancia de lectura entre 1,5 y 2 m** desde la cámara XR al panel.

**Pasos en Unity:**
1. Entra en **modo Play** (o quédate en Editor con la cámara XR activa).
2. Coloca la cámara XR (o el punto de vista del jugador) a **1,5–2 m** del panel. Usa las herramientas de medida de Unity o un objeto de referencia (cilindro/cubo de 1 m) si necesitas evidenciar la distancia visualmente.
3. En la ventana Scene, muestra el panel como un objeto 3D en la escena (no pegado a la cámara), con su Transform y la cámara XR a esa distancia.
4. Captura de modo que se vea a la vez: el panel, el Canvas (World Space) y la relación cámara→panel. Si cabe, incluye una pequeña anotación o gizmo de distancia.

**Justificación de la elección:**  
El texto dice que los paneles están *"situados en el mundo virtual, con Canvas en modo World Space"*. World Space es el único modo de Canvas que sitúa la UI como un objeto 3D con posición y escala reales en la escena, permitiendo colocar el panel a una distancia física (1,5–2 m) y que el usuario lo lea como parte del entorno.

**Por qué World Space y no otro modo de Canvas:**
- **No Screen Space Overlay:** se pegaría a la cámara, no tendría presencia espacial ni distancia de lectura propia; no encajaría con "paneles situados en el mundo virtual".
- **No Screen Space Camera:** dependería de una cámara específica y tampoco tendría posición independiente en el mundo; menos adecuado para paneles espaciales en RV.

**Por qué 1,5–2 m:** es el rango de lectura cómodo en realidad virtual (evita fatiga visual y permite leer texto TMP a escala real). Distancias menores obligan a girar la cabeza excesivamente; mayores hacen el texto ilegible.

**Criterio de aceptación:** se identifica el Canvas en World Space, el prefab `EmptyUIBackplateWithCanvas`, y la cámara XR está a 1,5–2 m del panel (verificable por Transform o gizmo).

---

### 2.3 `interfaz_rayo.png`

**Qué debe mostrar:**  
Selección a distancia mediante rayo:
- El **rayo** (línea de punteo) desde la mano o el mando dirigido al panel.
- El rayo incidindo sobre un control del panel (botón, desplegable o slider).
- Un **resultado observable**: por ejemplo, el `TMP_Dropdown` de locomoción abierto, o el `SmallSlider_LabelsAndIcons` del volumen movido (cambio de valor).
- Nota: con controladores, la confirmación usa el **botón de disparo (trigger)**, no el grip (ese se reserva para recoger fruta).

**Pasos en Unity (modo Play):**
1. Entra en Play con el panel visible y a distancia (1,5–2 m).
2. Activa el rayo desde el controlador o la mano.
3. Apunta a un control. Para un resultado claro y observable, recomiendo:
   - Opción A: apuntar al **desplegable de locomoción** (`TMP_Dropdown`) y abrirlo con el trigger → se ve la lista desplegada (resultado observable inequívoco).
   - Opción B: apuntar al **slider de volumen** y moverlo → se ve el valor cambiar y (si hay audio) escucharse el cambio de `AmbientVolumeController.SetAmbientVolume()`.
4. Captura el momento en que el rayo toca el control **y** el resultado es visible.

**Justificación de la elección:**  
La sección se titula *"Selección a distancia mediante rayo"* y menciona `ISDK_RayCanvasInteraction` + `RayInteractable`. El texto exige explícitamente *"Mostrar el rayo sobre un control y un resultado observable, como el desplegable abierto o un cambio de valor del slider"*. Por eso no basta con dibujar el rayo: debe verse el efecto de la interacción.

**Por qué rayo con trigger y no grip:**  
El texto distingue claramente: *"Con controladores, la selección por rayo utiliza el botón de disparo, a diferencia del grip utilizado para recoger fruta en el simulador."* Usar el grip aquí confundiría las dos interacciones del proyecto.

**Por qué no poke en esta captura:**  
Poke es la interacción cercana (sección siguiente). Mezclar rayo y poke en una misma imagen diluiría la demostración de cada modo.

**Criterio de aceptación:** se ve el rayo sobre un control y se observa un cambio de estado (dropdown abierto o slider movido) consecuencia directa de ese rayo.

---

### 2.4 `interfaz_poke.png`

**Qué debe mostrar:**  
Interacción cercana por contacto:
- El **dedo o la punta del mando** tocando físicamente la superficie del panel / un botón.
- El componente `ISDK_PokeCanvasInteraction` con `PokeInteractable` sobre el Canvas.
- La **respuesta del control** (botón presionado, toggle activado, slider movido, etc.), no solo la presencia del componente.

**Pasos en Unity (modo Play):**
1. Entra en Play y acerca la mano o el mando al panel hasta que el dedo/punta toque un botón o control.
2. Activa la pulsación por contacto (poke).
3. Captura en el instante del contacto **con la respuesta visible** (p. ej. el toggle de instrucciones cambia de estado y se abre/cierra el panel de la receta mediante `HelpDialogController.SetHelpVisible()`, o el toggle de efectos activa/desactiva `SpatialEffectsController.SetSpatialEffects()`).
4. Asegúrate de que se nota el contacto físico (no un rayo a distancia).

**Justificación de la elección:**  
La sección se titula *"Interacción cercana"* y describe el poke como *"acercar el dedo o la punta del mando al panel, sin depender del apuntado desde lejos"*. El texto es tajante: *"Verificar y mostrar su respuesta, no solo la presencia del componente"*. Por eso la captura debe mostrar el contacto y su efecto, no solo el componente `PokeInteractable` en el Inspector.

**Por qué no rayo aquí:**  
El rayo es a distancia (sección anterior). El poke es precisamente lo opuesto: interacción por proximidad/contacto. Cada captura debe demostrar un modo distinto.

**Criterio de aceptación:** se ve el dedo/punta del mando en contacto con el panel y el control responde (cambio de estado visible).

---

## 3. Justificación global: ¿por qué capturar desde Unity / el juego propio?

La memoria describe una implementación concreta: prefabs del Meta UI Set **adaptados**, scripts propios conectados (`AmbientVolumeController`, `LocomotionModeController`, etc.), Canvas en World Space, raycast del SDK de Meta y poke. Por eso:

| Fuente de la imagen | ¿Vale para esta memoria? | Motivo |
|---------------------|--------------------------|--------|
| **Unity Editor / juego propio (este plan)** | ✅ Sí | Muestra la implementación real: prefabs adaptados, scripts asignados, Canvas World Space, comportamientos reales (rayo/poke con resultado). |
| Imágenes de stock / mockups del Meta UI Set | ❌ No | No representan tus prefabs adaptados ni tus scripts conectados. |
| Capturas solo de un ejecutable/build | ⚠️ Parcial | Sirve para comportamiento en ejecución, pero no muestra Inspector/jerarquía (necesario en `interfaz_editor` y `interfaz_espacial`). |
| Imágenes generadas por IA | ❌ No | No son evidencia técnica de tu proyecto. |

**Conclusión:** se capturan desde la interfaz de Unity (Editor para montaje; Play mode para interacción en ejecución) porque solo allí existe la implementación real que la memoria debe documentar.

---

## 4. Alternativas descartadas por captura (tabla rápida)

| Captura | Alternativa descartada | Motivo del descarte |
|---------|------------------------|---------------------|
| `interfaz_editor.png` | Play mode | La memoria exige "en el Editor" y "no prueba en ejecución". |
| `interfaz_editor.png` | Solo ventana Game | No mostraría Inspector, jerarquía, LayoutGroup ni scripts asignados. |
| `interfaz_espacial.png` | Canvas Screen Space Overlay | No tendría presencia espacial ni distancia de lectura física. |
| `interfaz_espacial.png` | Sin cámara XR de referencia | No se podría justificar los 1,5–2 m de lectura. |
| `interfaz_rayo.png` | Solo rayo sin resultado | El texto exige "resultado observable" (dropdown abierto / slider movido). |
| `interfaz_rayo.png` | Grip en vez de trigger | El grip es para recoger fruta; el rayo usa disparo (trigger). |
| `interfaz_poke.png` | Solo componente en Inspector | El texto exige mostrar la respuesta del control, no solo el componente. |
| `interfaz_poke.png` | Rayo a distancia | Contradice la definición de poke como interacción cercana por contacto. |

---

## 5. Checklist de entrega

- [ ] `interfaz_editor.png`: Editor, panel, Canvas World Space, prefabs Meta UI Set, LayoutGroups, scripts asignados, TMP.
- [ ] `interfaz_espacial.png`: Panel en mundo, Canvas World Space, origen prefab `EmptyUIBackplateWithCanvas`, cámara XR a 1,5–2 m.
- [ ] `interfaz_rayo.png`: Play mode, rayo sobre control, resultado observable (dropdown abierto o slider movido), trigger (no grip).
- [ ] `interfaz_poke.png`: Play mode, contacto dedo/punta de mando con botón, respuesta visible del control.
- [ ] Todas las imágenes con nombre exacto y ruta esperada por LaTeX.

---

## 6. Nota sobre los scripts y controles (útil al escribir el pie de cada captura)

Al redactar las justificaciones en la memoria, puedes vincular cada captura a su script:

- **Volumen:** `SmallSlider_LabelsAndIcons` → `AmbientVolumeController.SetAmbientVolume()` (valor clamp 0–1).
- **Locomoción:** `TMP_Dropdown` → `LocomotionModeController.SetMode()` (Slide/Teleport/Ambos/Ninguno).
- **Instrucciones:** `ButtonShelf_IconAndLabel_Toggle` → `HelpDialogController.SetHelpVisible()` (panel de la receta).
- **Efectos espaciales:** toggle → `SpatialEffectsController.SetSpatialEffects()` (audio bathroom/pan/freír).
- **Actualización marcador:** `Dialog1Button_IconAndText` → `ProgressRefreshButton.cs` → `RefreshProgressUI()` (visual, no reinicia).
- **Salida:** `PrimaryButton` / `SecondaryButton` (confirmar/cancelar) → `ExitDialogController.cs` (`EditorApplication.isPlaying = false` en Editor, `Application.Quit()` en build).

Esto cierra la trazabilidad entre la imagen, el control visual y el script que lo mueve.
