# FomeDeGue API

## Visão Geral
Esta API foi desenvolvida em C# utilizando o framework ASP.NET Core para gerenciar o ecossistema do projeto FomeDeGue. O sistema provê funcionalidades robustas para gestão de contas de usuários, administração do catálogo de produtos e operações dinâmicas de carrinho de compras.

## Tecnologias e Frameworks
O projeto utiliza o **.NET 10.0** como plataforma principal de desenvolvimento para garantir alto desempenho e acesso às funcionalidades mais recentes da linguagem. Para o mapeamento objeto-relacional e persistência de dados, emprega-se o **Entity Framework Core** em conjunto com o motor de banco de dados **SQL Server** . O sistema de autenticação e autorização é gerenciado pelo **Microsoft Identity**, que lida com a identidade dos usuários de forma integrada ao contexto de dados.

## Bibliotecas Principais (Packages)
| Biblioteca | Versão | Finalidade |
| :--- | :--- | :--- |
| Microsoft.AspNetCore.Authentication.JwtBearer | 10.0.7 | Implementação de autenticação stateless baseada em tokens JWT. |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | 10.0.7 | Integração das funcionalidades do Identity com o banco de dados via EF Core. |
| Microsoft.EntityFrameworkCore.SqlServer | 10.0.8 | Provedor de dados para comunicação com instâncias do SQL Server. |
| Swashbuckle.AspNetCore | 6.5.0 | Ferramenta para geração automática de documentação interativa via Swagger. |

## Endpoints da API
Abaixo estão detalhados os endpoints disponíveis, organizados por contexto de funcionalidade.

### Gestão de Contas (v1/Account)
O controlador de contas centraliza a segurança e o perfil do usuário. Os métodos incluem o **Registro de Cliente** (POST /registrar-cliente) para novos usuários, o **Login** (POST /login) para autenticação. Os outros metodos do Auth, ainda não estão implementados, deixei para a sprint final **MARCELO LEIA AQUI**

### Catálogo de Produtos (v1/Product)
Este módulo é responsável pela manutenção do inventário do sistema. As operações disponíveis permitem **Criar Produto** (POST /criar-produto), **Consultar Produto por ID** (GET /{id}), **Remover Produto** (DELETE /deletar-produto/{id}), **Listar todos os Produtos** (GET /listar-produtos) e **Atualizar Informações** existentes (PUT /atualizar-produto/{id}).

### Carrinho de Compras (v1/Cart)
Gerencia a experiência de compra do usuário final. Os endpoints permitem **Adicionar Item ao Carrinho** (POST /{clientId}/items), **Visualizar Carrinho Atual** (GET /{clienteId}), **Reduzir Quantidade** de um item específico (POST /{clientId}/items/reduce) e **Remover Item** completamente do carrinho (DELETE /{clientId}/items/{productId}).

## Boas Práticas e Arquitetura
A aplicação adota padrões de design de software modernos para assegurar a escalabilidade e a facilidade de manutenção.

**Arquitetura em Camadas**: O projeto utiliza a separação clara entre controladores, lógica de negócio através de **Service Layer** e acesso a dados via **Repository Pattern**. **Injeção de Dependência**: A aplicação faz uso intensivo do container de DI nativo, registrando serviços e repositórios como `Scoped` para garantir que cada requisição tenha suas próprias instâncias. **Segurança com JWT**: A autenticação utiliza o padrão **JSON Web Token**, configurado com validação rigorosa de assinatura, emissor, audiência e expiração. **Documentação Automática**: O **Swagger** está configurado para expor os contratos da API de forma visual, facilitando o consumo por parte do frontend e integrando autenticação Bearer para testes. **Política de CORS**: O sistema está configurado para permitir requisições de origens externas, essencial para a integração com aplicações frontend.
