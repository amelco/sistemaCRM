# Sistema CRM
Pensei o seguinte, o diferencial da nossa ferramente será a venda por módulos, após a validação da ideia. Com base nisso pensei em seguir os passos abaixo: bora discutir isso aí.
1. Implementar o crud simples do cliente salvando os dados em sqlite, por enquanto
2. CRUD de módulos (agenda) só para associar e desassociar de um cliente
3. Login com controle de usuário, pra poder liberar os módulos (pra esse primeiro momento podemos controlar pelo banco mesmo)
4. Funcionalidade de agenda

## Arquitetura
Pensando na característica de vários clientes usando a mesma aplicação, mas cada cliente podendo ter diferentes módulos habilitados, temos alguma alternativas: 
- aplicação *multi-tenant*: O mesmo banco é compartilhado entre todos os clientes (*tenants*). Uma coluna (`'tenantId'` por exemplo) indentifica o tenant dono daquele registro da tabela.
- **aplicação *single-tenant* ou convencional: cada cliente terá seu próprio banco de dados. Acho que essa é nossa escolha já que usaremos banco de dados sqlite.**

Uma característica importante da escolha do sqlite é que ele é local, no sentido de ficar na mesma máquina que o executa, em nosso caso, na máquina onde o backend será *deployado*.

O problema com essa abordagem é que, se o arquivo sqlite ficar na mesma máquina do backend, o arquivo poderá sofrer problemas de consistência, uma vez que a cada deploy a máquina do backend é criada do zero (no caso de containers docker) consequentemente apagando o arquivo do banco e subindo um novo vazio. Soluções para esse problema:
1. Caso o backend seja remoto: ter todos os arquivos de banco de todos os clientes em um servidor de arquivos isolado. A string de conexão para essa arquivo sqlite apontaria para essa máquina remota. Isso traz problemas de segurança se não for bem gerenciado.
2. Caso o backend seja executado na máquina local do cliente: o cliente teria o arquivo de banco de dados em sua máquina local. O problema é a falta de backup, já que ninguém faz backup.

Então temos as seguintes opcoes, caso desejemos ir na rota single-tenant com banco sqlite:
1. Entregar uma solução 100% offline (aplicação de navegador), com modo on-line opcional para algumas funcionalidades (busca de CEP, por exemplo)
2. Entregar uma solução 100% online (site)

Ou então não utilizar o sqlite, e sim um mysql, postgres ou sqlserver (o mais barato):
1. Com somente um banco de dados e seguir o padrão multi-tenant
2. Com um banco de dados para cada cliente

**==> Consegue pensar em outras alternativas?**


### Módulo Base
- Definir o que contém o módulo base, isto é, o mínimo que nosso sistema vai oferecer. Por enquanto será:
  - CRUD de clientes

### Módulo Agenda
- Discutir funcionalidades
