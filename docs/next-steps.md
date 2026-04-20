# AXON · Próximos passos

Checklist vivo do projeto. Este arquivo deve ser atualizado a cada sessão que altere o estado de implementação do MVP.

## Concluído

- [x] Criar a solution `.NET` do AXON
- [x] Criar os projetos iniciais em `src/`, `plugins/` e `tests/`
- [x] Definir os contratos mínimos do domínio em `Axon.Core`
- [x] Implementar o engine inicial de execução em `Axon.Application`
- [x] Adicionar interpretador mock de intenção em `Axon.AI`
- [x] Criar registry inicial de plugins em `Axon.Infrastructure`
- [x] Criar plugin inicial `SystemActions`
- [x] Expor API local mínima com health, plugins e execução de comandos
- [x] Iniciar Web UI mínima em `Axon.Web`
- [x] Alinhar a interface visual do `Axon.Web` ao protótipo `temp/Axon_App_UI_v2.html`
- [x] Adicionar build e testes iniciais da solução

## Próximos passos

- [ ] Conectar a Web UI ao endpoint `POST /api/commands/execute`
- [ ] Modelar DTOs de aplicação para requests/responses do fluxo de comando
- [ ] Evoluir a seleção de plugins para usar metadados e compatibilidade de parâmetros
- [ ] Ampliar a descoberta de plugins para suportar carregamento futuro por assembly/diretório/manifests
- [ ] Estruturar configuração via Options Pattern para host, API, IA e plugins
- [ ] Adicionar observabilidade mínima com correlação de requisições e logs estruturados de ponta a ponta
- [ ] Criar testes de contrato para plugins e testes de integração do fluxo completo
- [ ] Implementar a integração leve com system tray no Windows para abrir a UI e indicar status do host
- [ ] Evoluir o plugin `HomeAssistant` além do placeholder inicial
- [ ] Integrar um interpretador LLM real atrás das abstrações existentes

## Notas de manutenção

- Marcar itens concluídos com `[x]` no mesmo conjunto de mudanças em que foram implementados.
- Adicionar novas tarefas descobertas durante a implementação.
- Remover ou reescrever itens obsoletos quando a direção do projeto mudar.
