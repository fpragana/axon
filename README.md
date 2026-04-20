# AXON

**AXON — Intelligence in motion.**

AXON é um assistente pessoal com IA executado localmente, focado em interpretar intenção, selecionar plugins e transformar comandos do usuário em ações reais de forma segura, extensível e desacoplada.

## Visão do projeto

O objetivo do AXON é atuar como um “cérebro operacional” local capaz de:

- receber comandos via texto;
- interpretar intenção com uma camada de IA isolada;
- decidir qual plugin deve executar a ação;
- acionar integrações locais ou externas;
- retornar uma resposta clara para a interface.

O MVP prioriza arquitetura sólida, extensibilidade e execução local antes de sofisticação de interface.

## Comportamento de execução local

Embora o AXON não seja inicialmente um aplicativo desktop tradicional, ele deve se comportar como um serviço residente amigável ao usuário no Windows.

Requisitos iniciais de experiência:

- o AXON deve exibir um ícone na bandeja do sistema do Windows, ao lado do relógio;
- o ícone deve indicar que o runtime local está carregado;
- o usuário deve conseguir abrir a interface visual a partir desse ícone;
- o menu do ícone deve evoluir para ações simples como abrir a UI, verificar status e encerrar o host com segurança.

Esse comportamento não substitui a Web UI: ele funciona como ponto de entrada e indicador de presença do serviço local.

## Objetivo do MVP

Entregar uma prova de conceito funcional que:

1. receba comandos via texto;
2. interprete intenção com IA ou mock de IA;
3. selecione um plugin compatível;
4. execute uma ação assíncrona;
5. devolva o resultado ao usuário por uma API local e uma interface web simples.

## Princípios de arquitetura

- separação total entre domínio, aplicação, IA, infraestrutura, host, API e UI;
- plugins desacoplados do núcleo do sistema;
- IA isolada das integrações concretas;
- tudo assíncrono por padrão;
- local-first;
- observabilidade e segurança desde o início.

As regras operacionais detalhadas do projeto estão em [`AGENTS.md`](./AGENTS.md).

## Stack planejada

### Backend

- .NET 10
- C#
- ASP.NET Core
- Hosted Services / Worker Services

### Frontend

- HTML
- CSS
- JavaScript

### IA

- abstrações próprias em `Axon.AI`;
- suporte a provedores locais ou remotos por adaptadores.

## Estrutura alvo

```text
Axon/
 ├── src/
 │   ├── Axon.Core/
 │   ├── Axon.Application/
 │   ├── Axon.Infrastructure/
 │   ├── Axon.AI/
 │   ├── Axon.Host/
 │   ├── Axon.Api/
 │   └── Axon.Web/
 │
 ├── plugins/
 │   ├── HomeAssistant/
 │   └── SystemActions/
 │
 ├── docs/
 ├── tests/
 ├── AGENTS.md
 ├── README.md
 └── .gitignore
```

## Fluxo principal

```text
Usuário → UI Web → API local → Engine AXON → IA interpreta intenção → Plugin executa → Resposta
```

## Presença no sistema operacional

No Windows, o AXON deve operar com dois papéis complementares:

- **host local residente** para manter API, engine e runtime ativos;
- **ícone na system tray** para sinalizar que o AXON está carregado e permitir abrir a interface no navegador.

Diretriz inicial:

- a UI principal continua sendo web;
- a integração com a bandeja é uma camada leve de experiência local, não uma migração para app desktop completo.

## Fases planejadas

### Fase 1 — MVP

- host local rodando;
- ícone na bandeja do Windows para indicar serviço ativo e abrir a interface;
- API HTTP local;
- interpretador mock de intenção;
- plugin de exemplo;
- execução básica de comandos.

### Fase 2

- integração real com LLM;
- plugin Home Assistant;
- melhor roteamento e seleção de ações.

### Fase 3

- interface web mais completa;
- entrada por voz;
- gerenciamento de plugins.

## Status atual

Projeto em fase de estruturação inicial.

Itens já definidos:

- direcionamento arquitetural em `AGENTS.md`;
- escopo inicial do MVP;
- stack base do projeto.

## Próximos passos

1. criar a solution `.NET`;
2. criar os projetos em `src/`;
3. definir contratos do domínio em `Axon.Core`;
4. implementar o engine de execução em `Axon.Application`;
5. adicionar interpretador mock de intenção;
6. criar plugin inicial `SystemActions`;
7. expor API local;
8. iniciar a Web UI mínima;
9. adicionar o ícone na bandeja do Windows para abrir a interface e indicar o estado do host.

## Licença

Licença ainda não definida.
