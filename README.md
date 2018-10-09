# architect-challenge

Desafio para vaga de arquiteto

Você deverá construir um sistema capaz de receber milhares de eventos por segundo. Um evento é definido pelo seguinte contrato (JSON):
```javascript
{
   "timestamp":"<Unix Timestamp>",
   "tag": "<string separada por '.' ex: brasil.sudeste.datacenter1.cpu0 >",
   "valor" : "<string>"
}
```
O campo timestamp é quando o evento ocorreu, a tag é o identificador do evento e o valor é o dado coletado de um determinado sensor, esses dados podem ser númericos ou string.

O seu programa deverá ser capaz de receber e armazenar eventos;

Será passado apenas 1 evento por vez;

Cada evento poderá ter o estado Processado ou Erro: Caso o campo valor chegue vazio o status do Evento será Erro caso contrário Processado;

Sua API deverá apresentar métricas sobre o volume de eventos recebidos por hora;

A visualização de dados deverão ser feitas através de gráficos apenas para os eventos com valor numérico e tabela para todos os dados recebidos;

Seu projeto deverá ter a possibilidade fazer o somatório acumulativo para eventos com valor numérico;

A visualização deverá ser realtime;

Sua solução deverá permitir consultas baseadas no prefixo da tag, exemplo: tag=brasil.sudeste.datacenter01.cpu0 deve suportar chave de consulta por brasil.sudeste.*

Sua solução deverá suportar consultas temporais baseada no timestamp do evento;

No seu README, você deverá fazer uma explicação sobre a solução encontrada, tecnologias envolvidas, instrução de uso da solução.

Desejável, todo o processo de build e subida deverão ser automatizados com make ou ferramenta similar.

Sua solução deverá ser desenvolvida em dotnet core 2.1+, além disso, a solução deverá ser disponibilizada através de containers (Docker)
