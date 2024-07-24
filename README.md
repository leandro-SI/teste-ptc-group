# Teste prático ptc-group

Teste prático C# Desenvolvimento de Blog, contendo 2 projetos, o primeiro um back-end com algumas funcionalidades básicas de um blog, e o segundo um front-end para testar e visualizar as notificações de criação de post via backend.


# API back-end

## Pré-requisitos

✔ - .NET 6 SDK

✔ - SQL Server

## Passos para instalação - backend

1. Clone o repositório:
```
- git clone https://github.com/leandro-SI/teste-ptc-group.git
```

2. Ajustar a connection string no aquivo appsettings.json conforme suas configurações de banco:
```
  "ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-HJ11OTL\\SQLEXPRESS;Database=BlogPTC;Trusted_Connection=True;"
  },
```

3. Gerar as migrations do banco de dados no backend a partir da camada BlogPTC.Infra.Data e rodar o projeto:
```
- update-database
```
<br>

Toda a documentação está disponível no Swagger, que pode ser acessado ao executar o projeto. É necessário registrar um usuário e realizar o login para obter um token de acesso JWT Bearer. Esse token deve ser incluído no cabeçalho de todas as requisições.

Exemplo: Authorization: Bearer 73467826478247824

# Front-End para testar as Notificaões

O back-end possui uma funcionalidade para envio de notificação para cada novo Post criado, sendo assim, eu criei um pequeno front-end para testar e visualizar essas notificações.

## Pré-requisitos

✔ - Node.js v18+


## Passos para instalação - frontend

1. Instale as dependências do front-end:
```
- Abra um terminal de comando dentro da pasta principal do front (Client-Notification) 
- rode o comando - npm install
```

2. Executando o front-end:
```
- npm start
```

3. Visualize o front:
```
- Abra seu browser de preferencia, e acesse a rota localhost com a porta do servidor: Exemplo: http://localhost:3000
```

Sendo assim, você poderá visualizar notificações no front (console e alert) para cada post criado no backend
