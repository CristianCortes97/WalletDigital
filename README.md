# WalletDigital API

API REST para la gestión de billeteras digitales y transferencias de saldo entre usuarios. Desarrollada en .NET 8 siguiendo Clean Architecture y buenas prácticas de desarrollo.

# Tecnologias Usadas
.NET 8
CLEAN ARCHITECTURE
SQL SERVER
Xunit

# la base de datos esta adjunta en el archivo script.ipynb

# End Point Principales:

# Wallet
GET/Wallet/GeAll -> Listar billeteras
GET/Wallet/GetWalletByIdAsync -> obtener billeras por Id
POST/Wallet/Create -> Crear billetera
POST/Wallet/TransferFunds -> Transferencias entre billeteras
PUT/Wallet/Update -> Actualizar billetera
DELETE/Wallet/Delete -> Eliminar billetera

# Transactions
GET/Wallet/GetAll -> Listar las transacciones
POST/Wallet/Transactions/Create -> Crear transacciones
