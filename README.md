# Desafio para vaga de arquiteto

## Considerações Gerais

* Sua solução deverá ser desenvolvida em dotnet core 2.1+.

* Devemos ser capazes de executar sua solução em um ambiente novo e de forma automatizada. Descrever no README como o projeto deve ser executado, requisitos de ambiente e demais informações necessárias para a execução.

* Considere que já temos o seguinte ambiente:
    * Windows 10 Professional
    * Ubuntu 18.0.4
    * .NET Core 2.1

* No seu README, você deverá fazer uma explicação sobre a solução encontrada, tecnologias envolvidas e instrução de uso da solução. 

* É interessante que você também registre ideias que gostaria de implementar caso tivesse mais tempo.

## Problema

Imagine que você ficou responsável por construir um sistema que seja capaz de receber milhares de eventos por segundo de sensores espalhados pelo Brasil, nas regiões norte, nordeste, sudeste e sul. Seu cliente também deseja que na solução ele possa visualizar esses eventos de forma clara.

Um evento é defino por um JSON com o seguinte formato:

```json
{
   "timestamp": <Unix Timestamp ex: 1539112021301>,
   "tag": "<string separada por '.' ex: brasil.sudeste.sensor01 >",
   "valor" : "<string>"
}
```

Descrição:
 * O campo timestamp é quando o evento ocorreu em UNIX Timestamp.
 * Tag é o identificador do evento, sendo composto de pais.região.nome_sensor.
 * Valor é o dado coletado de um determinado sensor (podendo ser numérico ou string).

## Requisitos

* Sua solução deverá ser capaz de armazenar os eventos recebidos.
> Atendido através da API: `http://[servidor]/api/v1/Event (POST)`

* Considere um número de inserções de 1000 eventos/sec. Cada sensor envia um evento a cada segundo independente se seu valor foi alterado, então um sensor pode enviar um evento com o mesmo valor do segundo anterior.
> Foi se pensado em utilizar o Rabbit MQ/Kafka para que houvesse um gerenciamento (através de filas) da grande quantidade de requisições por segundo (juntamente com um limitador *consumer*). Como o nginx fará o balanceamento de carga, ao iniciar múltiplas instâncias da API (ao menos 1 para cada região) o problema possa ser menor.

* Cada evento poderá ter o estado processado ou erro, caso o campo valor chegue vazio, o status do evento será erro caso contrário processado.
> Regra definida na camada de domínio e sempre executada antes da gravação do evento.

* Sua API deverá apresentar métricas sobre o volume de eventos recebidos por hora.
> Estas métricas podem ser obtidas através da API: `http://[servidor]/api/v1/Event/GetEventsGroupedByHour`

* Para visualização desses dados, sua solução deve possuir:
    * Uma tabela que mostre todos os eventos recebidos. Essa tabela deve ser atualizada automaticamente.
    > A lista com todos os eventos será retornada pela API: `http://[servidor]/api/v1/Event (GET)`
    * Um gráfico apenas para eventos com valor numérico.
    > Eventos numéricos são obtidos pela API: `http://[servidor]/api/v1/Event/GetNumericEvents`

* Para seu cliente, é muito importante que ele saiba o número de eventos que aconteceram por região e por sensor. Como no exemplo abaixo:
    * Região sudeste e sul ambas com dois sensores (sensor01 e sensor02):
        * brasil.sudeste - 1000
        * brasil.sudeste.sensor01 - 700
        * brasil.sudeste.sensor02 - 300
        * brasil.sul - 1500
        * brasil.sul.sensor01 - 1250
        * brasil.sul.sensor02 - 250
> A lista com o agrupamento por região/sensão é disponibilizada pela API: `http://[servidor]/api/v1/Event/GetAllGroupedByTag`

## Avaliação

Nossa equipe de desenvolvedores irá avaliar código, simplicidade da solução, testes unitários, arquitetura e automatização de tarefas.

Tente automatizar ao máximo sua solução. Isso porque no caso de deploy em vários servidores, não é interessante que tenhamos que entrar de máquina em máquina para instalar cada componente da solução.

Em caso de dúvida, entre em contato com o responsável pelo seu processo seletivo.

## Solução

* Foi criada uma aplicação com as seguintes tecnologias:
    * Back-end em .NET Core 2.1 *(seguindo a restrição definida no trecho referente ao ambiente)*
        * WebAPI no padrão REST com documentação feita via Swagger.
        * EntityFramework como ORM e banco de dados MySql.
    - [x] Feito
    * Front-end em Angular 11
    - [ ] A desenvolver

## Como utilizar
> **Requisito:** É necessário ter o docker instalado em seu sistema operacional (Linux, Windows ou Mac)

### Disponibilizar apenas o back-end
Rode o comando (apontando a pasta local do arquivo):  
- ` docker-compose -f docker-compose.backend.yml up --build --scale main-api=4` 

### Disponibilizar front-end e back-end
Rode o comando (apontando a pasta local do arquivo):  
- ` docker-compose -f docker-compose.yml up --build --scale main-api=4` 

#### É possível renomear um dos arquivos para 'docker-compose.yml' e rodar o comando mais simplificado:  
- ` docker-compose up --build --scale main-api=4` 

#### Para parar a execução no console (executando no modo 'detached'):  
- ` docker-compose down` 

#### Para parar a execução no console (executando no modo 'attached'):  
- <kbd>Crtl</kbd> + <kbd>C</kbd>