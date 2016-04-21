# cs-desafio.net
Desafio de implementação em .NET


# Desafio .NET

Crie um aplicativo backend que exporá uma API RESTful de criação de três endpoints (rotas):
- SignUp 
- Login
- Profile

## Requisitos

- Banco de dados em memória.
- Gestão de dependências via gerenciador de pacotes NuGet.
- Utilização de Web API 2 como framework.
- Persistência com ferramenta de ORM (EntityFramework, nHibernate, etc...).
- .NET ^ 4.* C# ^ 5.0
- Todos os endpoints devem trabalhar somente com JSONs. 
- Utilizar status codes de acordo com o tipo de resposta para a requisição.

## Requisitos desejáveis

- JWT como token generator
- Testes unitários
- Criptogafia não reversível (hash) na senha e no token
- Todas as respostas de erro devem retornar o seguinte conteúdo:
{ 
	"statusCode: 0, (StatusCode adequado)
	"mensagem": "sample string" (Mensagem adequada)
}
- A resposta de erro para endpoints não localizados também deverá ser um JSON contendo seu StatusCode adequado.
- Design Pattern desenvolvimento conhecido
- Injeção de dependência


Segue a documentação dos endpoints:

### SignUp - Criação de Cadastro

- Este endpoint deverá receber um usuário com os seguintes campos:
{ 
	"nome": "sample string",
	"email": "sample string",
	"senha": "sample string",
	"telefones": [
		{
			"numero": "sample string",
			"ddd": "sample string"
		}
	]
}
  

- Em caso de sucesso retornar os dados do usuário cadastrado e incluir os seguintes campos:
  - "id": id do usuário (pode ser o próprio gerado pelo banco, porém seria interessante se fosse um GUID)
  - "data_criacao": data da criação do usuário
  - "data_atualizacao": data da última atualização do usuário
  - "ultimo_login": data do último login (no caso da criação, será a mesma que a criação)
  - "token": token de acesso da API (pode ser um GUID ou um JWT)
- Caso o e-mail já exista, deverá retornar erro com a mensagem "E-mail já existente".
- O token deverá ser persistido junto com o usuário

### Login

- Este endpoint irá receber um objeto com os seguintes campos: 
{
	"email": "sample string",
    	"senha": "sample string",
}

- Caso o e-mail e a senha estejam corretas, retornar os mesmos dados do endpoint de SignUp.
- Caso o e-mail não exista, retornar erro com status apropriado mais a mensagem "Usuário e/ou senha inválidos"
- Caso o e-mail exista mas a senha não bata, retornar o status apropriado 401 mais a mensagem "Usuário e/ou senha inválidos"
- Atualizar o campo de ultimo_login deste usuário para a data atual

### Profile

- Chamadas para este endpoint devem conter um header na requisição de Authentication com o valor "Bearer {token}" onde {token} é o valor do recebido através do SignUp ou Login de um usuário.
- Caso o token não exista, retornar erro com status apropriado com a mensagem "Não autorizado".
- Caso o token exista, buscar o usuário pelo id passado através da query string e comparar se o token do usuário encontrado é igual ao token passado no header.
	- Caso não seja o mesmo token, retornar erro com status apropriado e mensagem "Não autorizado"
- Caso seja o mesmo token, verificar se o último login foi a MENOS que 30 minutos atrás.
	- Caso não seja a MENOS que 30 minutos atrás, retornar erro com status apropriado com mensagem "Sessão inválida".
- Caso tudo esteja ok, retornar os dados do usuário.

# 


