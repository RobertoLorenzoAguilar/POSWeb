# POSWeb
Proyecto punto de venta

Proyecto Web, tipo API.

#Diagrama entidad relación

 <img src="Images/e-r-1.PNG" alt="Logo">

# Se configura un proyecto tipo ASP.Net Web API con Swagger habilitado
Es una herramienta de software de código abierto para diseñar, construir, documentar, y utilizar servicios web RESTful.
Cuando se encuentra en modo desarrollo en program.cs se pueden visualizar los end-points habilitados.
<img src="Images/api.PNG" alt="Logo">

```csharp
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
```

# Generando Venta
<img src="Images/venta.PNG" alt="Logo">

# TODO:
1) Acomodar los script en carpetas separadas
2) crear el modelo relacional
3) checar la normalización de la base de datos
4) corregir bug del autocompletado de los productos o servicios
5) corregir porque no muestra la sucursal del empleado en el select
6) agregar la creación de tablas query
7) agregar el campo de teléfonos tipo multivalorado funcionalidad a sucursal
9) incorporar CRUD para ofertas temporales y logica

# NICE TODO:
1) Agreger pruebas unitarias
2) Agregar pruebas de regresión
3) Incorporar un Login no simulado
