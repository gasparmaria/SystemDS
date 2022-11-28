<h1>Sistema Dragon Sushi</h1>
<br>
<p>O presente sistema foi desenvolvido para o restaurante japonês Dragon Sushi, utilizando ASP.NET MVC.<br>
Desenvolvido pela empresa Westen, composta por Gabriel Soares, Guilherme Ferreira, Guilherme Lemes, Gustavo Basilio, Henrique Roncon e Maria Eduarda Gaspar</p>
<br>
<p>O banco de dados é local, sendo necessário executar o arquivo "script.sql", localizado na pasta <a href="https://github.com/gasparmaria/SystemDS/blob/main/Database/script.sql">"DataBase"</a>, no MySQL</p>
<br>
<p>O sistema possui 3 níveis de acesso: Gerente, Funcionário e Usuário.</p>
<br>
<h2>Gerente:</h2>
<p>Logo após o login, o gerente é levado à "Área do Gerente", onde poderá realizar as atividades:</p>
<ul>
	<li>Cadastro de Produtos (poderá cadastrar produtos);</li>
	<li>Cadastro de Funcionário (poderá cadastrar funcionários);</li>
	<li>Estoque (poderá visualizar o estoque com seus produtos, e editá-los caso queira);</li>
</ul>
<br>
<h2>Funcionário:</h2>
<p>Logo após o login, o funcionário é levado à "Área do Funcionário", onde poderá realizar as atividades:</p>
<ul>
	<li>Comandas (vizualizará a todas as comandas abertas naquele momento)</li>
		<span>Após selecionar uma comanda, será levado à tela de detalhes da comanda, onde poderá ver os pedidos cadastrados nela e seu valor total, onde poderá realizar seu pagamento, fechando-a.</span>
	<li>Reservas(vizualizará todas as reservas marcadas e seus detalhes, podendo editá-las ou excluí-las, além de cadastrar novas)</li>
	<li>Cadastro de Clientes(poderá cadastrar clientes);</li>
	<li>Lançar Pedidos(poderá cadastrar um pedido em uma comanda);</li>
</ul>
<br>
<h2>Usuário:</h2>
<p>Caso possua cadastro, poderá fazer login e será direcionado à Home. Se não, poderá se cadastrar.</p>
<ul>
	<li>Home (vizualizará os produtos do cardápio divididos por categoria)</li>
		<span>Cada produto, além de suas informações, possui um ícone de carrinho que redireciona o usuário a uma tela onde ele poderá cadastrar seu pedido</span>
	<li>Carrinho (opção no header, vizualizada no ícone de perfil)</li>
		<span>Vizualizará todos os itens adicionados ao carrinho e seu total, e poderá finalizar seu pedido.</span>
		<span>Ao clicar em finalizar pedido, será redirecionado a uma tela onde deve inserir as informações de pagamento e endereço, para realização do delivery.</span>
	<li>Editar perfil (opção no header, vizualizada no ícone de perfil -> poderá editar as informações do usuário logado)</li>
	<li>Sair (opção no header, vizualizada no ícone de perfil -> será redirecionado à tela de login)</li>
</ul>
