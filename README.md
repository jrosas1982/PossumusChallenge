# PossumusChallenge
El mismo consiste en una api con un CRUD, simple, con 2 tablas con relación.

Propuesta de ejercicio - API de RRHH
Utilizando las siguientes herramientas:
• .NET Framework 4.5 o superior / .NetCore 2.0 o superior
• MSSQL
• EntityFramework y Migraciones
• Swagger
Desarrollar una API Rest que permita gestionar las siguientes entidades:
Candidato: nombre, apellido, fecha de nacimiento, email, teléfono
Empleo: id, nombre-empresa, periodo
Relaciones
Candidato 1:* Empleo
La API deberá contar de un endpoint para listar los candidatos con sus empleos, otro para crear un empleado
junto a sus empleos, otro para modificar un empleado y por último uno para eliminar. (CRUD)
Logs
Aplicar algún mecanismo de log para registrar las acciones realizadas en la aplicación en un archivo externo.
Puede ser el incluido en el framework o alguna librería externa, por ejemplo, Serilog.
