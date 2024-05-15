# POSWeb
Proyecto punto de venta

Proyecto Web, tipo API.

#Diagrama entidad relación

 <img src="Images/e-r-1.PNG" alt="Logo">

# Se configura un proyecto tipo ASP.Net Web API con Swagger habilitado
Cuando se encuentra en modo desarrollo en program.cs se peuden visualizar los end-points habilitados
<img src="Images/api.PNG" alt="Logo">

```csharp
f (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



# TODO:
1) Acomodar los script en carpetas separadas
2) crear el modelo relacional
3) checar la normalización de la base de datos
4) corregir bug del autocompletado de los productos o servicios
