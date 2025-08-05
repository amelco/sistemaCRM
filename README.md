# Sistema CRM
Link pro trello: [Board](https://trello.com/b/Or5dsGFg/sistemacrm)

Pensei o seguinte, o diferencial da nossa ferramente será a venda por módulos, após a validação da ideia. Com base nisso pensei em seguir os passos abaixo: bora discutir isso aí.
1. Implementar o crud simples do cliente salvando os dados em sqlite, por enquanto
2. CRUD de módulos (agenda) só para associar e desassociar de um cliente
3. Login com controle de usuário, pra poder liberar os módulos (pra esse primeiro momento podemos controlar pelo banco mesmo)
4. Funcionalidade de agenda

## Arquitetura
Pensando na característica de vários clientes usando a mesma aplicação, mas cada cliente podendo ter diferentes módulos habilitados, temos alguma alternativas: 
- aplicação *multi-tenant*: O mesmo banco é compartilhado entre todos os clientes (*tenants*). Uma coluna (`'tenantId'` por exemplo) indentifica o tenant dono daquele registro da tabela.
- **aplicação *single-tenant* ou convencional: cada cliente terá seu próprio banco de dados. Acho que essa é nossa escolha já que usaremos banco de dados sqlite.**

### Estrutura
A filosofia do funcionamento do sistema é de ser agnóstico a qualquer coisa externa a ele. Isso é feito através da construção de interfaces (APIs) bem definidas em que o core do sistema pode se comunicar.
**Core** é tudo o que é parte do domínio, ou seja do negócio, e lá e **somente lá** ficarão as *regras de negócio*.

![alt text](https://github.com/amelco/sistemaCRM/blob/develop/documentation/arch_v1.png?raw=true)

Vamos dar um exemplo de como o sistema irá acessar o armazenamento para persitência e coleta de dados de forma agnóstica:  
Uma API será definida para armazenar entidades. Por exemplo:
```csharp
iterface IStorage<T>
{
  void Gravar<T>(T entidade);
  void Atualizar<T>(int id, T entidadePartial);
  void Excluir(int id);
  T Ler<T>(int id);
  List<T> Listar(Filtro filtro, Paginacao pag);
}
```

Dessa forma, se quisermos utilizar o sqlite para armazenamento, implementamos uma classe concreta dessa interface:
```csharp
class StorageSqlite<T> : IStorage<T>
{
  StorageSqlite() {
    // implementação do construtor
  }

  // implementação dos métodos da interface
  void Gravar<T>(T entidade) {
    // implementacao do método
  }

  void Atualizar<T>(int id, T entidadePartial) {
    // implementacao do método
  }

  void Excluir(int id) {
    // implementacao do método
  }

  T Ler<T>(int id) {
    // implementacao do método
  }

  List<T> Listar(Filtro filtro, Paginacao pag) {
    // implementacao do método
  }
}
```

O mesmo é feito com outras partes do sistema como UI e Módulos.

Pensando nisso, sugiro uma estrutura parecida com essa:
```
/
core/
    entities/
            Cliente.cs
            ...
    ...
storage/
       IStorage.cs
       SqliteStorage.cs
       ...
backend/
       clientController.cs
       ...
ui/
  IUi.cs
  UiWeb.cs
  UiDesktop.cs
  ...
modules/
       module1/
              IModule1.cs
              Module1.cs
      module2/
             ...
...    
```

## Cliente
Uma característica importante da escolha do sqlite é que ele é local, no sentido de ficar na mesma máquina que o executa, em nosso caso, na máquina onde o backend será *deployado*.

O problema com essa abordagem é que, se o arquivo sqlite ficar na mesma máquina do backend, o arquivo poderá sofrer problemas de consistência, uma vez que a cada deploy a máquina do backend é criada do zero (no caso de containers docker) consequentemente apagando o arquivo do banco e subindo um novo vazio. Soluções para esse problema:
1. **Caso o backend seja remoto:** ter todos os arquivos de banco de todos os clientes em um servidor de arquivos isolado. A string de conexão para essa arquivo sqlite apontaria para essa máquina remota. Isso traz problemas de segurança se não for bem gerenciado.
2. **Caso o backend seja executado na máquina local do cliente:** o cliente teria o arquivo de banco de dados em sua máquina local. O problema é a falta de backup, já que ninguém faz backup.

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
