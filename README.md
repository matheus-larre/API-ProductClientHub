# 🛒 ProductClientHub API

> API RESTful desenvolvida em **C# com .NET e Entity Framework Core**, para gerenciamento de clientes e produtos com relacionamento entre entidades.

---

## 📋 Sobre o Projeto

O **ProductClientHub** é uma API de cadastro e gerenciamento de **clientes** e **produtos**, onde cada produto pertence a um cliente. O projeto foi construído com arquitetura em camadas, separando responsabilidades entre API, comunicação (DTOs) e exceções customizadas.

---

## 🏗️ Arquitetura

O projeto é dividido em 3 camadas:

```
ProductClientHub/
├── ProductClientHub.API/           ← Camada principal: controllers, DbContext, entidades
├── ProductClientHub.Communication/ ← DTOs de Request e Response
└── ProductClientHub.Exceptions/    ← Exceções customizadas da aplicação
```

**Por que essa separação?**
- **API** — responsável por receber requisições HTTP e retornar respostas
- **Communication** — define os contratos de entrada/saída (evita expor entidades diretamente)
- **Exceptions** — centraliza os erros de negócio da aplicação

---

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Finalidade |
|---|---|
| C# 12+ | Linguagem principal |
| .NET 8+ | Framework da aplicação |
| ASP.NET Core | Framework web / API |
| Entity Framework Core | ORM para acesso ao banco |
| SQLite | Banco de dados local (arquivo `.db`) |
| Swagger / Swashbuckle | Documentação e teste dos endpoints |

---

## 🗃️ Modelo de Dados

```
Client (1) ──────── (N) Product
```

### Client
| Campo | Tipo | Descrição |
|---|---|---|
| Id | Guid | Identificador único (PK) |
| Name | string | Nome do cliente |
| Email | string | E-mail do cliente |
| Products | List\<Product\> | Produtos vinculados ao cliente |

### Product
| Campo | Tipo | Descrição |
|---|---|---|
| Id | Guid | Identificador único (PK) |
| Name | string | Nome do produto |
| Brand | string | Marca do produto |
| Price | decimal | Preço |
| ClientId | Guid | Chave estrangeira para Client (FK) |

> Ambas as entidades herdam de `EntityBase`, que fornece o campo `Id` (Guid).

---

## 🚀 Como Rodar Localmente

### Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET SDK 8+](https://dotnet.microsoft.com/download) — verifique com `dotnet --version`
- [Git](https://git-scm.com/)
- Um editor: [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

---

### Via Terminal

**1. Clone o repositório**
```bash
git clone https://github.com/matheus-larre/API-ProductClientHub.git
cd API-ProductClientHub
```

**2. Acesse a pasta da API**
```bash
cd ProductClientHub.API
```

**3. Restaure as dependências**
```bash
dotnet restore
```

**4. Execute o projeto**
```bash
dotnet run
```

O banco de dados SQLite será criado automaticamente na primeira execução.

**5. Acesse o Swagger**

Após subir o servidor, abra no navegador:
```
https://localhost:{porta}/swagger
```

> A porta exata é exibida no terminal após rodar `dotnet run`. Normalmente `5000`, `5001` ou `7000`.

---

### Via Visual Studio 2022

1. Abra o arquivo `ProductClientHub.slnx`
2. Clique com botão direito em `ProductClientHub.API` → **Set as Startup Project**
3. Pressione `F5` ou clique em **Run**
4. O Swagger abrirá automaticamente no navegador

---

## 📡 Endpoints

### Clients

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/clients` | Lista todos os clientes |
| `GET` | `/api/clients/{id}` | Busca cliente por ID |
| `POST` | `/api/clients` | Cadastra novo cliente |
| `PUT` | `/api/clients/{id}` | Atualiza cliente |
| `DELETE` | `/api/clients/{id}` | Remove cliente |

### Products

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/api/products` | Lista todos os produtos |
| `GET` | `/api/products/{id}` | Busca produto por ID |
| `POST` | `/api/products` | Cadastra novo produto |
| `PUT` | `/api/products/{id}` | Atualiza produto |
| `DELETE` | `/api/products/{id}` | Remove produto |

> Todos os endpoints podem ser testados diretamente pelo **Swagger UI**.

---

## 📁 Estrutura de Pastas

```
ProductClientHub/
│
├── ProductClientHub.API/
│   ├── Controllers/
│   │   ├── ClientsController.cs
│   │   └── ProductsController.cs
│   ├── Entities/
│   │   ├── EntityBase.cs
│   │   ├── Client.cs
│   │   └── Product.cs
│   ├── Infrastructure/
│   │   └── ProductClientHubDbContext.cs
│   ├── appsettings.json
│   └── Program.cs
│
├── ProductClientHub.Communication/
│   ├── Requests/
│   └── Responses/
│
└── ProductClientHub.Exceptions/
```

---

## ⚠️ Observações Importantes

- **Banco de dados:** o arquivo `.db` do SQLite é gerado automaticamente na primeira execução via `EnsureCreated()`. Não é necessária nenhuma configuração manual.
- **Sem autenticação:** a API está completamente aberta — sem JWT ou proteção de rotas.
- **Sem migrations:** mudanças nas entidades exigem deletar o arquivo `.db` e reiniciar o projeto para recriar o schema.

---

## 🔭 Possíveis Evoluções

- [ ] Adicionar autenticação com JWT
- [ ] Migrar para Migrations do EF Core
- [ ] Adicionar validações com FluentValidation
- [ ] Implementar paginação nas listagens
- [ ] Adicionar testes unitários com xUnit

---

## 👨‍💻 Autor

Desenvolvido por **Matheus Larré**

[![GitHub](https://img.shields.io/badge/GitHub-matheus--larre-181717?style=flat&logo=github)](https://github.com/matheus-larre)