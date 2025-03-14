# API de Gestão de Pedidos de Venda

Esta é uma API desenvolvida em **C# com .NET** para a gestão de pedidos de venda.

## 📌 Tecnologias e Bibliotecas Utilizadas

- **C# com .NET**
- **Mediator** para gerenciamento de comunicação entre componentes
- **Entity Framework Core** com **Migrations** para persistência em banco de dados relacional
- **PostgreSQL**
- **MongoDB**
- **RESTful API** seguindo boas práticas de arquitetura
- **Fluent Validation** para validaçao dos modelos de dados
- **xUnit** para testes unitários

## 📌 Instalação e Configuração

### Pré-requisitos

**IMPORTANTE**: Este projeto foi desenvolvido num sistema operacional **Windows**

Antes de rodar o projeto, certifique-se de ter instalado:

- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/community/)
- [.NET 8.0 ou superior](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Dotnet ef (CLI)](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) para aplicar as migrations

## 📌 Passos iniciais

1. **Abra a solução Ambev.DeveloperEvaluation.sln**
2. **Subindo as imagens docker**

   - Procure por **docker-compose**
   - Clique com o botão direito do mouse e vá em **Compose Up**

3. **Aplicando as migrations no banco de dados PostgreSQL**
   - Abra o terminal na pasta raíz (pelo Windows ou Visual Studio) e digite:
   ```bash
   cd .\src\Ambev.DeveloperEvaluation.ORM\
   dotnet ef database update --startup-project ..\Ambev.DeveloperEvaluation.WebApi
   ```
   - Espere o banco de dados ser atualizado
4. **Coloque o projeto Ambev.DeveloperEvaluation.WebApi para rodar**
5. **Clique** [aqui](https://localhost:7181/swagger/index.html) para abrir a documentação da API

## 📌 Usando os recursos

- Todos recursos/endpoints podem ser testados no link da propria documentação via Swagger. Mas antes, você precisa se cadastrar e obter o token de acesso.

  ### Criando o usuario

  - POST https://localhost:7181/api/Users
  - Body

  ```json
  {
    "username": "string",
    "password": "string",
    "phone": "string",
    "email": "string",
    "status": 0,
    "role": 0
  }
  ```

  ### Autenticando

  - POST https://localhost:7181/api/Auth
  - Body

  ```json
  {
    "email": "string",
    "password": "string"
  }
  ```

  ### Os demais recursos, podem ser explorados na documentação

  - api/Auth
  - api/Carts
  - api/Products
  - api/Sales
  - api/Users
  - **IMPORTANTE: Lembre-se se autenticar no Swagger utilizando o token obtido no passo anterior**

## 📌 Estrutura da aplicação

- Os recursos denominados Carrinho (**Carts**), Produtos (**Products**) e Usuários (**Users**) são mantidos na base de dados **PostgreSQL**
- O recurso Vendas (**Sales**) é mantido no **MongoDb**

## 📌 Considerações finais

- Antes de iniciar o desenvolvimento, precisei fazer algumas alterações no projeto existente (template), tais como:

  1. Precisei alterar a ConnectionString **DefaultConnection** no arquivo **appsettings.json** da API para o funcionamento correto no PostgreSQL. Acredito que estava configurado para SqlServer, pois o usuário estava definido como 'sa'
  2. Sobrescrevi algumas configurações do **docker-compose.yml**, tais como:

  - Portas de acesso (no Windows estava subindo aleatóriamente)
  - Nome da base de dados inicial do container Postgres
  - Desabilitado o container da Web Api
  - Adicionando volumes persistentes ao PostgreSQL, MongoDb e Redis

  3. Tive que excluir as **Migrations** existentes no projeto **Ambev.DeveloperEvaluation.ORM**, pois as mesmas não estavam subindo, dizia que as migrations já havia sido aplicadas no meu banco de dados, porem a base ainda continuava sem as tabelas necessárias.

- Percebi que existia uma instancia do Redis (cache) no docker-compose.yml. Como não foi citado utiliza-lo, não implementei. Mas se for necessário, basta inclui-lo no projeto para uso

  ```bash
  dotnet add package Microsoft.Extensions.Caching.StackExchangeRedis
  ```

  ```csharp
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = "localhost:6379";
        options.InstanceName = "MyApp_";
    });
  ```

  ```csharp
   public class Service(IDistributedCache cache){

     public void RemoveAndAddCache(string value){
       await cache.RemoveAsync("chave");
       await cache.SetAsync("chave", value, new DistributedCacheEntryOptions
       {
           SlidingExpiration = TimeSpan.FromMinutes(5) /
       });
     }

     public string GetFromCache(){
      var data = await cache.GetStringAsync("chave");
      return data;
     }

   }
  ```
