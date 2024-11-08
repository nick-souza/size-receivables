# Antecipação de Recebíveis - API Web

Este repositório contém uma API desenvolvida em C# com .NET 8, utilizando o Entity Framework (EF) para interação com o banco de dados MS SQL Server. O projeto segue o padrão de Repository, separando a lógica de controle, serviços e repositórios. A API também possui um middleware customizado para tratamento de erros e integração com Swagger para documentação da API.

## Tecnologias Utilizadas
- Backend: C# 10, .NET 8, Entity Framework Core, MS SQL Server
- Arquitetura: RESTful API, Repository Pattern
- Database: MS SQL Server
- Outros: Swagger, Docker Compose

## Instruções de Configuração
### Pré-requisitos
Certifique-se de que os seguintes itens estejam instalados:
- Docker
- Docker Compose
- .NET 8 SDK

### Como Executar a Aplicação com Docker
1. Clone o repositório.
2. Navegue até a pasta do projeto e execute o comando para build do projeto:
```
dotnet build
```
3. Para iniciar a aplicação com o Docker Compose, execute o seguinte comando:
```
docker-compose up --build
```

### Após a construção e inicialização do Docker:
- API: [http://localhost:5282/api/v1](http://localhost:5282/api/v1)
- Swagget [http://localhost:5282/swagger/index.html](http://localhost:5282/swagger/index.html)

### Configuração de Portas
- Backend: 5282
- Swagger 5282/swagger
- Banco de Dados: 1433 (SQL Server)

### Observações:
- Existe um script e ini.sql que serão executados quando o conteiner DB subir pela primeira vez, com tudo configurado.
- Dentro da pasta docs, existe um JSON que é possível importar uma coleção no PostMan, que já comtém os endpoints com alguns exemplos.

## Endpoints da API
### Companies (empresas)
| Método      | Endpoint           | Descrição  |
| ------------- |-------------|-----|
| GET | /company | Retorna todas as empresas |
| POST | /company | Cria uma nova empresa |
| GET | /company/{cnpj} | Retorna uma empresa pelo CNPJ |
| DELETE | /company/{cnpj} | Deleta uma empresa pelo CNPJ |
| PUT | /company/{cnpj} | Atualiza os dados de uma empresa |
| POST | /company/{cnpj}/checkout | Realiza o cálculo da antecipação de recebíveis para a empresa, passando uma lista de notas fiscais |

### Invoice (notas fiscais)
| Método      | Endpoint           | Descrição  |
| ------------- |-------------|-----|
| GET | /invoice | Retorna todas as notas fiscais |
| POST | /invoice | Cria uma nova nota fiscal |
| GET | /invoice/{numero} | Retorna uma nota fiscal pelo número |
| DELETE | /invoice/{numero} | Deleta uma nota fiscal pelo número |
| GET | /invoice/company/{cnpj} | Retorna todas as notas fiscais de uma empresa pelo CNPJ |

## Funcionalidades
### Cadastro de Empresas
As empresas podem ser cadastradas com os seguintes dados:
- CNPJ (obrigatório)
- Nome (obrigatório)
- Faturamento Mensal (obrigatório)
- Ramo (obrigatório: “Serviços” ou “Produtos”)

### Cadastro de Notas Fiscais
As empresas podem cadastrar notas fiscais com os seguintes dados:
- Número (obrigatório)
- Valor (obrigatório)
- Data de Vencimento (obrigatório, deve ser maior que a data atual)

### Cálculo de Limite de Antecipação

O limite de antecipação da empresa é calculado com base no seu faturamento mensal e ramo de atuação:

- Faturamento entre R$ 10.000,00 e R$ 50.000,00: 50% do faturamento.
- Faturamento entre R$ 50.001,00 e R$ 100.000,00:
  - Empresa de Serviços: 55% do faturamento.
  - Empresa de Produtos: 60% do faturamento.
- Faturamento acima de R$ 100.001,00:
  - Empresa de Serviços: 60% do faturamento.
  - Empresa de Produtos: 65% do faturamento.

## Cálculo de Antecipação (Checkout)

Para calcular o valor a ser antecipado, a fórmula utilizada é:

- Prazo: Data atual - Data de vencimento da nota fiscal (em dias).
- Taxa: 4,65% ao mês.
- Deságio: ValorNF / (1 + Taxa)^(Prazo / 30)
- Valor Líquido: ValorNF - Deságio

O sistema retorna um JSON com o valor final de antecipação para cada nota e o valor total do carrinho.
