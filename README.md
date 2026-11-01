# DocuTrack S.A. - Taller Integrador: Árboles Binarios (BST en C# MVC)

## Integrantes y Responsabilidades
* **Cesar Luis Taimbud Oviedo**: Desarrollo integral del proyecto.
  * **Capa Model (`Nodo.cs`, `ArbolBinario.cs`)**: Lógica estructural del Árbol Binario de Búsqueda (inserción, búsqueda con comparaciones, actualización eliminar+insertar, eliminación de casos 1, 2 y 3, recorridos y cálculo de altura).
  * **Capa View (`DocumentoView.cs`)**: Interfaz de consola, formateo de estados, presentación de recorridos y renderizado de la estructura jerárquica en ASCII con conectores (`├──`, `└──`, `│`).
  * **Capa Controller (`DocumentoController.cs`)**: Orquestación de casos de uso y batería de pruebas guiadas (construcción con 15 nodos, 6 búsquedas, 3 actualizaciones y 3 eliminaciones).

## Requisitos de Ejecución
* .NET SDK 8.0 o superior.
* Ejecución desde consola:
  ```bash
  dotnet run