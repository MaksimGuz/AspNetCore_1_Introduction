### swagger-codegen
https://stackoverflow.com/questions/21076179/pkix-path-building-failed-and-unable-to-find-valid-certification-path-to-requ
keytool -import -alias example -keystore  "C:\Program Files (x86)\Java\jre1.8.0_191\lib\security\cacerts" -file certificate.cer

java -jar swagger-codegen-cli-2.2.1.jar generate -i https://localhost:44354/swagger/v1/swagger.json -l csharp -o swagger-codegen
packages\NUnit.ConsoleRunner.3.9.0\tools\nunit3-console.exe src/IO.Swagger.Test/bin/Debug/IO.Swagger.Test.dll

### migrations
Add-Migration InitIdentity -Context ApplicationDbContext
Update-Database -Context ApplicationDbContext

### react + aspnet core 
http://www.talkingdotnet.com/create-react-app-visual-studio-2017-and-asp-net-core-2-2/
https://github.com/alimon808/contoso-university/tree/master/ContosoUniversity.Spa.React

### create resources automation
Login-AzureRmAccount
Get-AzureRmSubscription
Set-AzureRmContext -SubscriptionId "guid id"
.\Deploy-AzureResourceGroup.ps1 -ResourceGroupName "aspnetcoreMG" -ResourceGroupLocation "East US"

### iOS issue with oidc logins
https://github.com/aspnet/Security/issues/1864

### apply migrations on prod
https://dotnetplusplus.com/2018/09/23/deploying-a-code-first-entity-framework-database-in-azure-devops/

### docker development
https://github.com/dotnet/dotnet-docker/blob/master/samples/aspnetapp/aspnetcore-docker-https-development.md

docker stop $(docker ps -a -q)
docker rm $(docker ps -a -q)

docker run -it --rm -p 8000:80 mvc

docker login
docker tag mvc maksimguz/mvc
docker push maksimguz/mvc

### Web app for containers
az login
az account set --subscription <guid id>

az webapp create --resource-group docker-webapp-linux --plan mvc20181122035205Plan --name maksimguzaspnetcoredocker --deployment-container-image-name maksimguz/aspnetcore
az webapp config appsettings set --resource-group docker-webapp-linux --name maksimguzaspnetcoredocker --settings WEBSITES_PORT=80

### dockerfile issues/1864
https://github.com/aspnet/Announcements/issues/298