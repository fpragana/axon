# AGENTS.md

## Propósito deste repositório

AXON é um assistente pessoal com IA executado localmente, orientado a comandos, automação e plugins. O objetivo do MVP é transformar intenção em ação com o menor acoplamento possível entre interface, motor de execução, IA e integrações externas.

Este arquivo define como agentes de código devem trabalhar neste projeto para manter consistência arquitetural desde o início.

---

## Objetivo do MVP

Entregar uma prova de conceito funcional que:

1. receba comandos via texto;
2. interprete intenção com uma camada de IA isolada;
3. selecione um plugin compatível;
4. execute uma ação assíncrona;
5. retorne uma resposta clara ao usuário.

### Fora do escopo inicial

- interface desktop nativa;
- workflow visual complexo;
- marketplace online completo;
- voz obrigatória no primeiro ciclo;
- forte dependência de um provedor específico de LLM.

**Observação importante:** uma interface desktop completa continua fora do escopo inicial, mas o produto deve oferecer integração leve com a bandeja do sistema no Windows para indicar que o serviço está ativo e permitir abrir a interface web local.

---

## Princípios mandatórios

1. **Separação total de responsabilidades** entre Core, Application, AI, Infrastructure, Host, API e Web.
2. **Plugins desacoplados**: nenhuma regra de negócio central deve depender de plugin específico.
3. **IA isolada**: a camada de interpretação não executa integrações diretamente.
4. **Tudo assíncrono por padrão**: I/O, execução de ações, inferência, carregamento de plugins.
5. **Local-first**: o sistema deve rodar localmente com dependências mínimas.
6. **Extensibilidade antes de sofisticação**: estruturas e contratos devem favorecer crescimento futuro.
7. **Observabilidade desde o MVP**: logs estruturados, correlação básica e mensagens de erro objetivas.
8. **Segurança básica desde o início**: validar inputs, limitar execução de ações e evitar exposição indevida de capacidades sensíveis.

---

## Stack e diretrizes tecnológicas

### Backend

- .NET 10
- C#
- ASP.NET Core para API HTTP local
- Worker/Hosted Services para orquestração e tarefas em background

### Integração local com sistema operacional

- no Windows, prever um componente leve de system tray para expor o estado do host;
- esse componente não deve concentrar regra de negócio;
- sua responsabilidade é descoberta, presença do serviço e atalhos operacionais simples, como abrir a UI local.

### Frontend

- HTML, CSS e JavaScript puros no MVP
- UI responsiva, dark mode, visual minimalista/futurista
- sem dependência obrigatória de framework SPA na fase inicial

### IA

- camada própria em `Axon.AI`
- suporte a provedor remoto ou local via abstrações
- qualquer integração com OpenAI, Ollama ou similar deve entrar apenas por adaptadores

---

## Estrutura alvo do repositório

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
 └── README.md
```

Se o repositório estiver vazio, essa deve ser a direção inicial. Não criar estruturas paralelas fora desse desenho sem necessidade justificada.

---

## Responsabilidades por camada

### `Axon.Core`

Contém o núcleo estável do domínio.

**Deve conter:**
- entidades e value objects do domínio;
- contratos centrais;
- descritores de plugins e ações;
- modelos de comando, intenção e resultado;
- enums e constantes de domínio.

**Não deve conter:**
- HTTP;
- acesso a arquivo;
- SDK de IA;
- detalhes de banco, UI ou provedores externos.

### `Axon.Application`

Orquestra casos de uso do sistema.

**Deve conter:**
- handlers/use cases;
- serviços de roteamento e execução;
- DTOs de entrada/saída;
- validações de aplicação;
- interfaces consumidas pelo domínio e implementadas fora.

**Não deve conter:**
- lógica específica de transporte HTTP;
- código de plugin concreto;
- implementação direta de SDKs externos.

### `Axon.AI`

Camada especializada em interpretação de intenção.

**Deve conter:**
- abstrações para análise semântica;
- adaptadores de provedores de LLM;
- normalização da saída da IA;
- fallback mock para MVP/testes.

**Não deve conter:**
- chamadas diretas a plugins;
- decisão final de infraestrutura;
- regras de UI.

### `Axon.Infrastructure`

Implementa integrações técnicas e plugins.

**Deve conter:**
- loader/registry de plugins;
- acesso a filesystem quando necessário;
- adaptadores externos;
- implementação concreta de interfaces de aplicação.

**Não deve conter:**
- regras centrais que deveriam estar no Core/Application.

### `Axon.Host`

Composição local do sistema.

**Deve conter:**
- bootstrap;
- configuração de DI;
- hosted services;
- leitura de configurações locais;
- inicialização do runtime;
- coordenação do lifecycle do host residente.

**Pode conter, quando necessário para a experiência local no Windows:**
- inicialização do componente de bandeja do sistema;
- publicação de status básico do runtime para consumo pela integração de bandeja.

**Não deve conter:**
- regra central de interpretação de intenção;
- lógica de negócio de plugins.

### `Axon.Api`

Exposição HTTP local.

**Deve conter:**
- endpoints/controllers;
- mapeamento entre HTTP e casos de uso;
- contratos de request/response;
- health checks básicos.

**Não deve conter:**
- regra de negócio relevante;
- lógica específica de plugins;
- prompts de IA ou parsing semântico complexo.

### `Axon.Web`

Interface web acessada via navegador.

**Deve conter:**
- tela de comando textual;
- exibição da resposta;
- histórico básico de interação do cliente;
- identidade visual dark/ciano.

**Não deve conter:**
- decisão de negócio;
- segredos;
- dependência acoplada ao motor interno.

### Integração de bandeja do sistema (Windows)

Se implementada como componente separado, deve seguir estas regras:

**Deve conter:**
- ícone de presença ao lado do relógio do Windows;
- ação para abrir a interface visual no navegador padrão;
- estado simples do host, como carregando, ativo ou indisponível;
- menu mínimo com ações seguras e locais.

**Não deve conter:**
- engine de execução;
- decisão de roteamento de comandos;
- implementação de plugins;
- lógica de domínio.

---

## Modelo conceitual mínimo

Os agentes devem preservar estes conceitos centrais:

- **UserCommand**: comando bruto enviado pelo usuário.
- **IntentAnalysis**: resultado estruturado da interpretação.
- **PluginActionDescriptor**: descreve uma ação exposta por um plugin.
- **PluginExecutionRequest**: ação selecionada + parâmetros.
- **ExecutionResult**: sucesso, mensagem, metadados e erros.
- **IAxonPlugin**: contrato base de plugin.
- **IAxonPluginRegistry / Loader**: descoberta e catálogo de plugins.
- **IIntentInterpreter**: contrato da camada de IA.
- **ICommandExecutionEngine**: orquestrador principal.

---

## Regras para o sistema de plugins

O sistema de plugins é o pilar do projeto.

### Requisitos obrigatórios

1. Cada plugin expõe metadados claros: nome, descrição, ações e parâmetros esperados.
2. A descoberta de plugins deve permitir expansão futura por assemblies, diretórios ou manifesto.
3. O engine deve interagir com plugins via contrato estável, nunca por if/else hardcoded por plugin.
4. Ações devem ser explicitamente nomeadas e documentadas.
5. Parâmetros devem ser serializáveis e validáveis.
6. Execução de plugin deve aceitar cancelamento.

### Diretrizes de desenho

- preferir descritores tipados ao invés de dicionários soltos sempre que possível;
- se o MVP iniciar com `Dictionary<string, object>`, encapsular cedo em modelos próprios;
- separar **descoberta**, **seleção** e **execução** de plugins;
- plugins sensíveis (sistema operacional, automação residencial, shell) exigem validação e proteção explícitas.

### Contrato conceitual inicial

```csharp
public interface IAxonPlugin
{
    string Name { get; }
    string Description { get; }
    IReadOnlyCollection<PluginActionDescriptor> GetActions();
    Task<PluginExecutionResult> ExecuteAsync(
        string actionName,
        IReadOnlyDictionary<string, object?> parameters,
        CancellationToken cancellationToken = default);
}
```

Esse contrato pode evoluir, mas a evolução deve aumentar tipagem e segurança, não reduzir.

---

## Engine de execução

Fluxo esperado do sistema:

1. UI envia comando textual para API local.
2. API delega para um caso de uso de aplicação.
3. A aplicação solicita interpretação da intenção à camada de IA.
4. O engine seleciona a ação de plugin mais compatível.
5. O plugin executa a ação.
6. O resultado é normalizado e devolvido para UI.

### O engine deve fazer

- coordenar as etapas;
- registrar logs da decisão;
- tratar falhas previsíveis;
- retornar resposta consumível pela UI.

### O engine não deve fazer

- conhecer detalhes internos de um plugin específico;
- conter código HTTP;
- depender do frontend;
- embutir prompts rígidos em múltiplos pontos.

---

## Decisões de implementação recomendadas para o MVP

### API inicial

Priorizar endpoints simples como:

- `POST /api/commands/execute`
- `GET /api/plugins`
- `GET /api/health`

### Experiência local no Windows

Priorizar um ícone de system tray no MVP para:

- sinalizar ao usuário que o AXON está carregado;
- abrir a interface web local;
- expor ações simples de lifecycle, como encerrar o host com segurança.

### Plugins iniciais

1. **Mock/SystemActions** para validar a arquitetura
2. **HomeAssistant** em fase posterior, após o pipeline básico estar estável

### IA inicial

Implementar duas estratégias:

- **MockIntentInterpreter** para desenvolvimento e testes
- **LLMIntentInterpreter** atrás de interface/configuração

### Configuração

Usar `appsettings.json`, `appsettings.Development.json` e Options Pattern.

Nunca hardcode:

- chaves de API;
- caminhos absolutos;
- URLs de provedores;
- portas sem configuração ou defaults claros.

---

## Convenções de código

1. Usar `async/await` em toda operação I/O bound.
2. Toda API pública assíncrona deve aceitar `CancellationToken` quando fizer sentido.
3. Preferir interfaces pequenas e orientadas a caso de uso.
4. Manter classes focadas e coesas.
5. Evitar arquivos gigantes; dividir responsabilidades cedo.
6. Usar logging estruturado.
7. Tratar erros com mensagens objetivas e sem vazar detalhes sensíveis.
8. Modelos compartilhados entre camadas devem ser mínimos e estáveis.
9. Evitar reflexão desnecessária no núcleo; se usada para plugins, isolar na infraestrutura.
10. Não introduzir frameworks pesados no frontend antes de validar o MVP.

---

## Estratégia de testes

Todo trabalho relevante deve considerar testes desde o início.

### Prioridades

1. testes unitários para engine, roteamento e interpretação mock;
2. testes de integração para API local;
3. testes de contrato para plugins;
4. testes básicos da UI apenas quando houver interação significativa.

### Casos críticos

- comando válido com plugin compatível;
- comando ambíguo;
- comando sem plugin correspondente;
- falha de plugin;
- timeout/cancelamento;
- indisponibilidade do provedor de IA com fallback configurado.

---

## Segurança e proteção operacional

Mesmo no MVP, assumir postura defensiva.

- validar e sanitizar entradas do usuário;
- restringir ações perigosas em plugins;
- não executar comandos arbitrários sem whitelist ou política explícita;
- separar capacidades descritivas de capacidades executáveis;
- registrar auditoria mínima de execução;
- manter segredos fora do código-fonte.

---

## Observabilidade mínima

Cada fluxo principal deve gerar logs com:

- comando recebido;
- resultado da análise de intenção;
- plugin/ação escolhidos;
- duração da execução;
- status final;
- motivo de falha, quando houver.

Para a integração de bandeja no Windows, registrar também:

- inicialização do ícone;
- falha ao publicar estado do host;
- abertura da interface pelo atalho da bandeja;
- encerramento solicitado pelo usuário via menu local.

Evitar logs com conteúdo sensível completo quando isso puder expor credenciais ou dados pessoais.

---

## Ordem recomendada de implementação

Quando agentes forem evoluir o projeto, seguir preferencialmente esta ordem:

1. criar solution e projetos `.NET`;
2. definir contratos do Core;
3. implementar casos de uso da Application;
4. criar mock de IA;
5. criar registry/loader de plugins;
6. criar plugin de exemplo;
7. expor API local;
8. criar Web UI mínima;
9. adicionar integração de bandeja no Windows para abrir a UI e indicar status do host;
10. adicionar observabilidade, configuração e testes;
11. então integrar LLM real.

---

## Critérios de aceite do MVP

O MVP está consistente quando:

- um comando textual pode ser enviado pela UI ou API;
- a intenção é interpretada por uma abstração de IA;
- um plugin é selecionado sem acoplamento rígido;
- a ação executa de forma assíncrona;
- a resposta retorna ao usuário com sucesso ou erro tratável;
- a solução pode crescer com novos plugins sem refatoração estrutural ampla.

---

## Diretriz para futuros agentes de código

Ao implementar qualquer parte do AXON:

1. preservar a arquitetura em camadas;
2. não mover lógica de domínio para controllers ou UI;
3. não acoplar IA diretamente a plugins;
4. não criar atalhos que inviabilizem o marketplace futuro;
5. preferir evolução incremental com contratos sólidos;
6. documentar decisões relevantes em `README.md` ou `docs/`;
7. se houver dúvida entre velocidade e extensibilidade, escolher a solução mais simples que preserve a extensibilidade.

---

## Resumo operacional

AXON deve nascer como uma plataforma local, modular e extensível, com um núcleo pequeno, um orquestrador claro, uma camada de IA substituível e um sistema de plugins como principal mecanismo de expansão. Toda contribuição deve reforçar essa direção.
