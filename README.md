# SupermarketApi
Api REST en .NET Core 5 con MySQL con categorias y productos.

*Post productos:<br/>
Recibe valores en int que luego serán convertidos en la siguiente enumeración:<br/>
Product Post UnitofMeasurement Enum :<br/>
1: Unity | 2: Miligram | 3: Gram | 4 :Kilogram | 5: Liter<br/>
Al solicitar por get devuelve una abreviación del mismo en string.

## Dependencias
- [EntityFramework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
- [MySQL EF connector](https://www.nuget.org/packages/MySql.EntityFrameworkCore/)
- [AutoMapper](https://www.nuget.org/packages/AutoMapper/)

## Ejemplos
-GET CATEGORIES:<br/>
![Get categories](https://github.com/EmilianoGM/SupermarketApi/blob/master/REST%20imagenes/categorias.jpg)
<br/>
-POST CATEGORIES:<br/>
![post categories](https://github.com/EmilianoGM/SupermarketApi/blob/master/REST%20imagenes/newCategory.jpg)
<br/>
-GET PRODUCTS:<br/>
![get products](https://github.com/EmilianoGM/SupermarketApi/blob/master/REST%20imagenes/productos.jpg)
<br/>
-POST PRODUCTS:<br/>
![post products](https://github.com/EmilianoGM/SupermarketApi/blob/master/REST%20imagenes/newProduct.jpg)
