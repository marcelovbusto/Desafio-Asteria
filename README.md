# **Desafio Asteria**

Bem-vindo ao **Desafio Asteria**, um sistema robusto e eficiente para importar e analisar dados de vendas. Este projeto foi desenvolvido utilizando tecnologias modernas, como .NET 8, EF Core, Chart.js e Bootstrap 5, para oferecer alta performance e visualizações interativas.

---

## **Índice**

- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Requisitos](#requisitos)
- [Configuração do Banco de Dados](#configuração-do-banco-de-dados)
- [Configuração do Projeto](#configuração-do-projeto)
- [Rodando o Projeto](#rodando-o-projeto)
- [Funcionalidades Principais](#funcionalidades-principais)
- [Exemplo de Uso](#exemplo-de-uso)
- [Contribuindo](#contribuindo)

---

## **Tecnologias Utilizadas**

- **.NET 8.0**: Framework principal para desenvolvimento do backend.
- **Entity Framework Core**: Gerenciamento do banco de dados e operações CRUD.
- **EFCore.BulkExtensions**: Otimização para inserções em massa no banco de dados.
- **SQL Server**: Banco de dados relacional confiável e escalável.
- **Chart.js**: Biblioteca para gráficos dinâmicos e interativos.
- **Bootstrap 5**: Framework front-end para design responsivo e moderno.
- **Font Awesome**: Ícones modernos para melhorar a interface do usuário.

---

## **Requisitos**

Antes de começar, certifique-se de que você possui os seguintes softwares instalados:

- **SQL Server** (ex.: Microsoft SQL Server 2019 ou superior)
- **Visual Studio 2022** (ou superior, com suporte ao .NET 8.0)
- **.NET SDK 8.0**
- **Node.js** (se necessário para dependências front-end)
- **Navegador moderno** (ex.: Google Chrome)

---

## **Configuração do Banco de Dados**

1. Acesse o SQL Server utilizando sua ferramenta preferida (ex.: SQL Server Management Studio).
2. Crie o banco de dados com o seguinte comando:

    ```sql
    CREATE DATABASE sales;
    ```

3. Configure a string de conexão no arquivo `appsettings.json` do projeto:

    ```json
    "ConnectionStrings": {
        "SqlServer": "Server=localhost;Database=sales;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
    }
    ```

4. Execute as migrações para criar as tabelas no banco de dados:

    ```bash
    dotnet ef database update
    ```

---

## **Configuração do Projeto**

1. Clone o repositório:
    ```bash
    git clone [git@github.com:marcelovbusto/Desafio-Asteria.git](https://github.com/marcelovbusto/Desafio-Asteria.git)](https://github.com/marcelovbusto/Desafio-Asteria.git
    cd desafio-asteria
    ```

2. Restaure as dependências:
    ```bash
    dotnet restore
    ```

3. Compile o projeto:
    ```bash
    dotnet build
    ```

---

## **Rodando o Projeto**

1. Certifique-se de que o SQL Server está em execução.
2. Inicie o servidor:
    ```bash
    dotnet run
    ```

3. Acesse o sistema no navegador utilizando o https.

---

## **Funcionalidades Principais**

### **1. Importação de Planilhas**
- Suporte a arquivos Excel contendo dados de vendas.
- Suporta grandes volumes de dados com **EFCore.BulkExtensions**.

### **2. Relatórios de Vendas**
- Listagens detalhadas com filtros personalizáveis (mês, cliente, categoria, SKU, etc.).
- Dados organizados em tabelas responsivas.

### **3. Gráficos Interativos**
- Exibição de gráficos dinâmicos usando **Chart.js**.
- Gráficos de barras com eixos duplos para análise de valores e quantidades.

### **4. Paginação**
- Navegação paginada para melhorar a usabilidade e a performance.

---

## **Exemplo de Uso**

1. Acesse o sistema no Visual Studio 2022 iniciando pelo https.
2. Navegue até a página **"Importar Planilha"**.
3. Faça upload de um arquivo Excel com os seguintes campos (ordem esperada):
   - **Data**, **Código Cliente**, **Categoria**, **SKU**, **Produto**, **Quantidade**, **Valor**.
4. Após a importação, visualize os dados em:
   - **Listagem de Vendas**: Tabela detalhada com filtros.
   - **Relatórios Trimestrais**: Gráficos e análises detalhadas.

---


## **Contato**

- **E-mail**: marcelovbusto@hotmail.com

---

Com esta documentação, qualquer desenvolvedor pode facilmente configurar, rodar e entender o propósito do **Desafio Asteria**. 🚀
Qualquer dúvida estou a disposição!
