River Book application

ConnectionString = Server=127.0.0.1;Port=5432;Database=myDataBase;Integrated Security=true;

### migration command

dotnet ef migrations add InitialUsers -c UserDBContext -p ../RiverBooks.Users/RiverBooks.Users.csproj -s ./RiverBooks.Web.csproj -o Data/Migrations 

dotnet ef database update -c UserDBContext

dotnet ef database update -c UserDBContext -p ../RiverBooks.Users/RiverBooks.Users.csproj -s ../RiverBooks.Web/RiverBooks.Web.csproj

dotnet ef migrations add AddCartItems -c UserDBContext -p ../RiverBooks.Users/RiverBooks.Users.csproj -s ../RiverBooks.Web/RiverBooks.Web.csproj


dotnet ef migrations add AddInitialOrder -c OrderDBContext -p ../RiverBooks.OrderProcessing/RiverBooks.OrderProcessing.csproj -s ../RiverBooks.Web/RiverBooks.Web.csproj