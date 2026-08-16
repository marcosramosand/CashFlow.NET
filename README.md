## Sobre o projeto

***API*** de Gerenciamento de Despesas Pessoais desenvolvida em **.NET 8**, seguindo os princípios do ***Domain-Driven Design (DDD)***.
A solução foi projetada com uma arquitetura limpa e orientada ao domínio, priorizando organização, manutenibilidade e escalabilidade. Seu objetivo principal é permitir que os usuários registrem e organizem suas despesas de forma prática e eficiente.
Funcionalidades

***Cadastro de despesas com as seguintes informações: Título, Data e hora, Descrição, Valor e Tipo de pagamento***

Persistência segura dos dados em banco de dados ***MySQL***

A ***API*** oferece uma base sólida para o controle financeiro pessoal, com foco em clareza de código, segurança no armazenamento dos dados e facilidade de evolução do sistema.



A arquitetura da **API** baseia-se em **REST**, utilizando métodos **HTTP** padrão para uma comunicação eficiente e simplificada. Além disso, é complementada por uma documentação **Swagger**, que proporciona uma interface gráfica interativa para que os desenvolvedores possam explorar e testar os endpoints de maneira fácil.

Dentre os pacotes NuGet utilizados, o **AutoMapper** é o responsável pelo mapeamento entre objetos de domínio e requisição/resposta, reduzindo a necessidade de código repetitivo e manual. O **Fluent Assertions** é utilizado nos testes de unidade para tornar as verificações mais legíveis, ajudando a escrever testes claros e compreensíveis. Para as validações, o **FluentValidation** é usado para implementar regras de validação de forma simples e intuitiva nas classes de requisições, mantendo o código limpo e fácil de manter. Por fim, o **EntityFramework** atua como um ORM (Object-Relational Mapper) que simplifica as interações com o banco de dados, permitindo o uso de objetos .NET para manipular dados diretamente, sem a necessidade de lidar com consultas SQL