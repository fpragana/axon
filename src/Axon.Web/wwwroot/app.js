const input = document.getElementById('commandInput');
const output = document.getElementById('previewOutput');
const button = document.getElementById('runPreview');

button?.addEventListener('click', () => {
  const value = input?.value?.trim() ?? '';

  if (!value) {
    output.textContent = 'Informe um comando de exemplo para visualizar o fluxo.';
    return;
  }

  output.textContent = [
    'Fluxo previsto para o comando:',
    `1. UI envia: ${value}`,
    '2. API local recebe o comando',
    '3. Axon.Application solicita análise ao interpretador mock',
    '4. Plugin SystemActions é selecionado quando aplicável',
    '5. Resultado normalizado retorna para a interface',
  ].join('\n');
});
