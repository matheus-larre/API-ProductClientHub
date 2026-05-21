# 🛒 ProductClientHub API

> API RESTful desenvolvida em **C# com .NET 10 e Entity Framework Core**, para gerenciamento de clientes e produtos com relacionamento entre entidades, segurança JWT e arquitetura profissional.

---

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-black?style=for-the-badge&logo=jsonwebtokens&logoColor=000000)
![SQLite](https://img.shields.io/badge/SQLite-07405E?style=for-the-badge&logo=sqlite&logoColor=white)
![xUnit](https://img.shields.io/badge/xUnit-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

---

## 📋 Sobre o Projeto

O **ProductClientHub** é uma solução robusta para o cadastro e gerenciamento de **clientes** e seus respectivos **produtos**. O projeto segue princípios de **Clean Code**, **SOLID** e utiliza uma arquitetura em camadas para garantir escalabilidade e facilidade de manutenção.

### 🌟 Diferenciais desta Versão
- **Segurança:** Autenticação via JWT com hash de senhas (BCrypt).
- **Escalabilidade:** Paginação implementada nos endpoints de listagem.
- **Confiabilidade:** Base de testes unitários com xUnit, Moq e FluentAssertions.
- **Profissionalismo:** Gerenciamento de banco de dados via EF Core Migrations.

---

## 🏗️ Arquitetura

O projeto é dividido em camadas bem definidas:

```
ProductClientHub/
├── ProductClientHub.API/           ← Camada principal: Controllers, Use Cases, Infra e Entidades
├── ProductClientHub.Communication/ ← Contratos (DTOs) de Entrada (Requests) e Saída (Responses)
├── ProductClientHub.Exceptions/    ← Gerenciamento centralizado de exceções de negócio
└── ProductClientHub.Tests/         ← Testes unitários de Use Cases
```

---

## 🛠️ Tecnologias e Bibliotecas

| Tecnologia | Finalidade |
|---|---|
| **.NET 10** | Runtime e Framework base |
| **EF Core 10** | Mapeamento Objeto-Relacional (ORM) |
| **JWT Bearer** | Autenticação e Autorização segura |
| **BCrypt.Net** | Hashing seguro de senhas |
| **FluentValidation** | Validações de domínio fluídas e injetáveis |
| **Bogus** | Geração de dados aleatórios para testes |
| **Moq** | Criação de objetos simulados para testes unitários |
| **SQLite** | Banco de dados leve e relacional |

---

## 🚀 Como Rodar Localmente

### Pré-requisitos
- [.NET SDK 10+](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)

### Passo a Passo

**1. Clone o repositório**
```bash
git clone https://github.com/matheus-larre/API-ProductClientHub.git
cd API-ProductClientHub
```

**2. Restaure as dependências**
```bash
dotnet restore
```

**3. Configure o Banco de Dados (Migrations)**
```bash
dotnet ef database update --project ProductClientHub.API
```

**4. Execute o projeto**
```bash
dotnet run --project ProductClientHub.API
```

**5. Acesse o Swagger**
Abra no navegador: `https://localhost:{porta}/swagger`

---

## 🧪 Como Rodar os Testes

Para garantir que tudo está funcionando corretamente:
```bash
dotnet test
```

---

## 📡 Endpoints Principais

### Auth
- `POST /api/Login` - Autentica usuário e retorna o Token JWT.

### Clients (Requer Token)
- `GET /api/clients?pageNumber=1` - Lista clientes com paginação.
- `GET /api/clients/{id}` - Detalhes de um cliente específico.
- `POST /api/clients` - Cadastra novo cliente.
- `PUT /api/clients/{id}` - Atualiza dados do cliente.
- `DELETE /api/clients/{id}` - Remove um cliente.

### Products (Requer Token)
- `POST /api/products` - Vincula um produto a um cliente.
- `DELETE /api/products/{id}` - Remove um produto.

---

## 👨‍💻 Autor

Desenvolvido por **Matheus Larré**

[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/matheus-larre)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/matheusrlarre/)
