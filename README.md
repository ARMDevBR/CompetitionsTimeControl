# Controle de Tempo para Competições Intervaladas
<p align="center">
  <img src="/Icons and Images/ProgramLogo.png">
</p>

## Introdução

O objetivo do programa é controlar o tempo dos intervalos (tiros) de uma competição (ou treino) por meio de beeps de contagem regressiva e um beep marcador de início do tiro.

Opcionalmente pode-se deixar uma playlist de músicas locais tocando de fundo.

Abaixo uma imagem mostrando como a tela é ao abrir pela primeira vez e um exemplo de como ela fica ao término da execução de uma programação de competição de 30 tiros de 2 minutos (120 segundos) cada tiro.

<p align="center">
    <img src="/Icons and Images/StartSample.png" hspace="10">
    <img src="/Icons and Images/EndSample.png" hspace="10" >
</p>

## Funcionalidades

**Opções gerais do software no menu "Configuração":**
- Abrir uma configuração previamente salva;
- Salvar novamente a configuração que foi aberta ou salva recentemente;
- Salvar a configuração com outro nome ou salvá-la pela primeira vez;
- Habilitar visualização de dicas nos textos dos controles ou botões ao mover o mouse sobre eles;
- Recarregar a lista de beeps disponível;

**Controles relacionados aos beeps marcadores de contagem regressiva e início:**
- Escolher o par (contagem/início) de beeps disponível;
- Definir se haverá um beep (o mesmo da 'contagem') no meio do intervalo para separar o período de esforço do período de descanso em tempos fixos (metade do tempo do tiro);
- Definir o tempo extra antes de iniciar os beeps;
- Definir o tempo extra após término do último beep;
- Definir a quantidade de beeps desejado;
- Definir o tempo para cada beep de contagem;
- Definir o volume máximo para os beeps;

**Controles relacionados às músicas de fundo:**
- Adicionar músicas na lista de músicas do programa (formatos permitidos: mp3, wav, wma);
- Excluir toda a lista de músicas do programa;
- Escolher quais músicas excluir marcando-as na lista após acionar a função adequada;
- Ouvir uma determinada música da lista selecionando a mesma após acionar a função adequada;
- Definir se as músicas vão tocar de forma sequencial ou randômica ao iniciar a competição com músicas de fundo;
- Visualizar a quantidade de músicas válidas, inválidas (que deixaram de existir ou mudaram de pasta, etc) e o tempo total estimado da playlist de músicas válidas;
- Definir os limites mínimo e máximo de volume para a música durante a competição. Estes limites são acionados, respectivamente, antes e após o evento dos beeps;
- Avançar ou voltar a linha de tempo da música bem como trocar para a próxima música ou para a anterior via mini-player na tela;

**Controles relacionados à execução da competição e a quantidade e tempo dos intervalos:**
- Definir se a competição vai acontecer apenas com beeps ou beeps e músicas de fundo;
- Definir se a competição com beeps e músicas de fundo inicia a playlist a partir da música que já estiver tocando ou da música inicial da playlist criada;
- Definir se a competição com beeps e músicas de fundo reinicia a playlist (caso a mesma termine antes da competição finalizar) de modo sequencial, randômico ou inverte o último modo;
- Definir o tempo em segundos para mudar o volume da música tocando para o limite mínimo, antes de iniciar os beeps de contagem, e para o limite máximo, ao finalizar o evento dos beeps;
- Definir se a competição já começa tocando os beeps como preparação para a largada ou se começa imediatamente com o evento dos beeps sendo acionados apenas a partir do fim do primeiro tiro;
- Definir se - ao finalizar a competição - as músicas de fundo param de tocar imediatamente ou continua tocando a música atual até a mesma finalizar;
- Definir a quantidade de intervalos que a competição ou treino vai ter;
- Definir o tempo em segundos que terá cada intervalo;

## Instalação

No momento disponível apenas no sistema operacional Windows. Foi testado no Windows 10 e Windows 11.

Este software não possui um instalador padrão, sendo apenas uma tela a ser executada. Para isto basta descompactar o arquivo [CompetitionsTimeControl.zip](/Download) em algum lugar no seu computador e abrir o programa **Controle de Tempo para Competições Intervaladas.exe** conforme mostrado na imagem abaixo.

<p align="center">
  <img src="/Icons and Images/ProgramInFolder.png">
</p>

## Pré-Requisitos

O software utiliza dois mini-players na tela, um para tocar os beeps e outro para as músicas. Estes controles utilizam o Windows Media Player do sistema operacional Windows. Portanto, o mesmo deve estar previamente instalado, o que costuma ser o padrão do sistema.

Se a tela do programa não abrir, um aviso será dado informando que o ".NET Desktop Runtime" precisa ser instalado e um link redirecionando para a página de download será mostrado. Aqui um exemplo do link para a versão [.NET 8.0 Desktop Runtime v8.0.7 - Windows x64 Installer](https://dotnet.microsoft.com/pt-br/download/dotnet/thank-you/runtime-desktop-8.0.7-windows-x64-installer?cid=getdotnetcore).

## Uso

Como usar...

## Licença

Este é um software livre sob licença GPL 3.0, podendo ser utilizado e modificado livremente.

## Autores e Reconhecimentos

Desenvolvido por Ademilson Rodrigo Moreira baseando-se na ideia inicial de Leandro Medeiros de Almeida.

O logotipo e ícone do programa foi gerado por IA através do [Copilot Designer](https://copilot.microsoft.com/images/create).
Os ícones dos botões são do site [Icons8](https://icons8.com/icon/set/forms-audio/fluency).

## Histórico de Versões

**Versão 1.1.0**
- Adicionado nova funcionalidade de beep no meio do intervalo, separando metade do tempo para esforço e a outra metade para o descanso;
- Adicionado nova funcionalidade que adiciona o nome do arquivo salvo ou lido depois do nome da tela na barra de título;
- Corrigido um bug aparente que congelava a tela ao iniciar uma competição com músicas e beeps de início ao mesmo tempo. Uma solução alternativa foi aplicada para que a competição comece com um pequeno atraso (imperceptível) após a música começar a tocar. O problema, que acontecia algumas vezes, parece ser com os players presentes na tela;
- Melhorado o sistema de "dicas de textos e botões" para não atualizar as dicas dinâmicas em caso da opção de dicas estar desabilitada no menu. Da forma anterior, ao clicar em um botão de dica dinâmica (o texto altera conforme a situação), a dica aparecia brevemente mesmo a opção de dicas estando desativada.

**Versão 1.0.2**
- Resolução de bug que impedia adicionar uma música caso o nome dela iniciasse com um nome que fosse parte do início de outra música previamente inserida;
- Adicionado ícone para o executável.

**Versão 1.0.1**
- Resolução de bug que mantinha a música tocando ao excluir toda a lista de músicas.

**Versão 1.0.0**
- Versão concluída com todas as funcionalidades previstas.

## Referências

Vídeo demonstrando a utilização do programa:
