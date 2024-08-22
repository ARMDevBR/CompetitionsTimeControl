# Controle de Tempo para Competições Intervaladas
<p align="center">
  <img src="/Icons and Images/ProgramLogo.png">
</p>

## Introdução

O objetivo do programa é controlar o tempo dos intervalos (tiros) de uma competição ou treino por meio de beeps de contagem regressiva e um beep marcador de início do tiro.

Opcionalmente pode-se deixar uma playlist de músicas locais tocando de fundo.

A versão atual do software é a 1.2.0 e a mesma pode ser baixada conforme as instruções na seção "Instalação" abaixo.

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
- Recarregar a lista de beeps disponível.

**Controles relacionados aos beeps marcadores de contagem regressiva e início:**
- Escolher o par (contagem/início) de beeps disponível;
- Definir se haverá um beep (o mesmo da 'contagem') no meio do intervalo para separar o período de esforço do período de descanso em tempos fixos (metade do tempo do tiro);
- Definir o tempo extra antes de iniciar os beeps;
- Definir o tempo extra após término do último beep;
- Definir a quantidade de beeps desejado;
- Definir o tempo para cada beep de contagem;
- Definir o volume máximo para os beeps;
- Usar os botões para ouvir a sequência de beeps configurada ou ouvir individualmente o beep de contagem, ou de início.

**Controles relacionados às músicas de fundo:**
- Adicionar músicas na lista de músicas do programa (formatos permitidos: mp3, wav, wma);
- Excluir toda a lista de músicas do programa;
- Escolher quais músicas excluir marcando-as na lista após acionar a função adequada;
- Ouvir uma determinada música da lista selecionando a mesma após acionar a função adequada;
- Definir se as músicas vão tocar de forma sequencial ou randômica ao iniciar a competição com músicas de fundo;
- Visualizar a quantidade de músicas válidas, inválidas (que deixaram de existir ou mudaram de pasta, etc) e o tempo total estimado da playlist de músicas válidas;
- Definir os limites mínimo e máximo de volume para a música durante a competição. Estes limites são acionados, respectivamente, antes e após o evento dos beeps;
- Avançar ou voltar a linha de tempo da música bem como trocar para a próxima música ou para a anterior via mini-player na tela.

**Controles relacionados à execução da competição e a quantidade e tempo dos intervalos:**
- Definir se a competição vai acontecer apenas com beeps ou beeps e músicas de fundo;
- Definir se a competição com beeps e músicas de fundo inicia a playlist a partir da música que já estiver tocando ou da música inicial da playlist criada;
- Definir se a competição com beeps e músicas de fundo reinicia a playlist (caso a mesma termine antes da competição finalizar) de modo sequencial, randômico ou inverte o último modo;
- Definir o tempo em segundos para mudar o volume da música tocando para o limite mínimo, antes de iniciar os beeps de contagem, e para o limite máximo, ao finalizar o evento dos beeps;
- Definir se a competição já começa tocando os beeps como preparação para a largada ou se começa imediatamente com o evento dos beeps sendo acionados apenas a partir do fim do primeiro tiro;
- Definir se - ao finalizar a competição - as músicas de fundo param de tocar imediatamente ou continua tocando a música atual até a mesma finalizar;
- Definir a quantidade de intervalos que a competição ou treino vai ter;
- Definir o tempo em segundos que terá cada intervalo.

## Instalação

No momento disponível apenas para o sistema operacional Windows. Foi testado nos Windows 10 e 11.

Este software não possui um instalador padrão, sendo apenas uma tela a ser executada.

Para baixar o arquivo, clique no link [CompetitionsTimeControl_v1.2.0.zip](/Download/CompetitionsTimeControl_v1.2.0.zip) e depois clique em "View raw" ou no botão de download conforme destacados em vermelho na imagem abaixo.

<p align="center">
  <img src="/Icons and Images/HowToDownload.png">
</p>

Após esta etapa, será solicitado um local no seu computador para salvar o arquivo compactado no formato .zip. Salve ele e depois descompacte os arquivos.
Para abrir o programa, vá até o local dos arquivos compactados e abra o **Controle de Tempo para Competições Intervaladas.exe** conforme mostrado na imagem abaixo.

<p align="center">
  <img src="/Icons and Images/ProgramInFolder.png">
</p>

Repare na imagem abaixo que, na mesma pasta onde está o programa de controle de tempo, também há uma pasta chamada "BeepSounds". A existência desta pasta é obrigatória.
Dentro da mesma existem várias pastas contendo os pares de beeps que aparecem como opção no programa. Também há um arquivo chamado "<u>Leia Me.txt</u>" que explica como funciona este mecanismo. É possível o próprio usuário adicionar outros pares de beeps se quiser.

<p align="center">
  <img src="/Icons and Images/BeepsSoundsFolder.png">
</p>

Também é possível baixar a pasta com todos os beeps pelo link [AllBeepSounds.zip](/Download/AllBeepSounds.zip) e depois clicar em "View raw" ou no botão de download da mesma maneira feito para baixar o programa **CompetitionsTimeControl_v1.2.0.zip**, como explicado acima nesta mesma seção.
Após baixado, basta descompactar e substituir a pasta "BeepSounds" existente.

Ao tentar executar o programa **Controle de Tempo para Competições Intervaladas.exe** pela primeira vez, pode ser solicitado uma verificação do arquivo pelo seu programa antivírus ou uma ação de confiança por parte do usuário. A primeira imagem abaixo mostra um aviso do Microsoft Defender SmartScreen. Ao clicar em "<u>Mais informações</u>", a tela atualiza para o mostrado na segunda imagem, agora com o botão "Executar assim mesmo" disponível. Este procedimento acontece porque o programa não foi instalado por um instalador padrão nem possui assinaturas de validação, etc. Mas basta varrer o arquivo com o seu antivírus para ver que o mesmo não apresenta risco nenhum.

<p align="center">
    <img src="/Icons and Images/MSDefender1.png" hspace="10">
    <img src="/Icons and Images/MSDefender2.png" hspace="10" >
</p>

Se não abrir o programa ainda, leia a seção "Pré-Requisitos" abaixo.

## Pré-Requisitos

O software utiliza dois mini-players na tela, um para tocar os beeps e outro para as músicas. Estes controles utilizam o Windows Media Player do sistema operacional Windows. Portanto, o mesmo deve estar previamente instalado, o que costuma ser o padrão do sistema.

O software foi criado com controles em posições e tamanhos fixos na tela, não possuindo redimensionamento automático. Por isto a tela não permite redimensionamento pelas bordas e nem maximizar a mesma.

Por este mesmo motivo, a tela foi projetada para resoluções maiores que a HD (1280 x 720) e com a escala ajustada para 100%. Se a resolução utilizada for menor ou a escala for maior que as recomendadas aqui, a tela pode ficar inutilizável.

Para verificar as configurações atuais, clique com o botão direito do mouse na imagem de fundo do seu desktop e escolha a opção "**Configurações de exibição**". Na tela que abrir, procure as configurações conforme mostrado abaixo na imagem representando a configuração no Windows 10.
<p align="center">
  <img src="/Icons and Images/ScreenScaleAndResolution.png">
</p>

Se a tela do programa não abrir, um aviso será dado informando que o ".NET Desktop Runtime" precisa ser instalado e um link redirecionando para a página de download será mostrado. Aqui um exemplo do link para a versão [.NET 8.0 Desktop Runtime v8.0.7 - Windows x64 Installer](https://dotnet.microsoft.com/pt-br/download/dotnet/thank-you/runtime-desktop-8.0.7-windows-x64-installer?cid=getdotnetcore).

Após esta etapa, o programa já estará pronto para funcionar. 

## Uso

OBS: Os gifs mostram a versão 1.1.1. A versão atual é a 1.2.0 e possui um elemento visual a mais ao iniciar a prova, não havendo diferença nas funcionalidades.

**Configurando e testando os beeps:**
- Os beeps são lidos automaticamente da pasta "BeepSounds" que se encontra na mesma pasta do programa "Controle de Tempo para Competições Intervaladas.exe". Leia a seção "Instalação" acima para mais informações;
- O "Beep de meio intervalo" só pode ser verificado durante a prova, pois depende do tempo do intervalo para ser acionado, não podendo ser testado nesta parte;
- Acione a opção "<u>Habilitar dicas de textos e botões</u>" no menu "**Configuração**" e deixe o mouse sobre o botão "Testar beeps" para entender bem como funcionam as configurações de tempo e quantidade de beeps.
<p align="center">
  <img src="/Icons and Images/BeepConfiguration.gif">
</p>

**Adicionando músicas (Opcional):**
- Pode-se adicionar músicas de várias pastas diferentes;
- Os formatos permitidos são "mp3", "wav" e "wma".
<p align="center">
  <img src="/Icons and Images/AddMusics.gif">
</p>

**Excluindo todas as músicas da lista de uma vez só:**
<p align="center">
  <img src="/Icons and Images/DeleteAllMusics.gif">
</p>

**Adicionando músicas, testando-as individualmente e ajustando volume:**
 - Repare na música listada em vermelho. Isto pode acontecer com músicas que tiveram a extensão trocada, foram renomeadas ou então a pasta raiz foi renomeada, ou excluída. É algo que será mais visto ao abrir um arquivo de configuração salvo anteriormente pelo menu **Configuração**.
<p align="center">
  <img src="/Icons and Images/AddAndListeningMusic.gif">
</p>

**Excluindo músicas individualmente:**
<p align="center">
  <img src="/Icons and Images/DeleteIndividually.gif">
</p>

**Testando iniciar uma prova só com beeps:**
<p align="center">
  <img src="/Icons and Images/JustBeeps.gif">
</p>

**Testando completar uma prova só com beeps:**
- Os beeps ao final do tiro são utilizados como preparação para o novo tiro. Portanto, não há beeps de transição no último tiro.
<p align="center">
  <img src="/Icons and Images/CompleteJustBeeps.gif">
</p>

**Iniciando prova com músicas e beeps:**
<p align="center">
  <img src="/Icons and Images/StartWithMusics.gif">
</p>

**Algumas maneiras de finalizar a prova com música tocando:**
<p align="center">
  <img src="/Icons and Images/FinishingCompetitionWithMusic.gif">
</p>

**Mostrando o funcionamento da opção de dicas habilitadas:**
<p align="center">
  <img src="/Icons and Images/EnableTips.gif">
</p>

**Mostrando o funcionamento da opção "Beep de meio intervalo":**
- Neste exemplo o beep acontece no tempo de 15 segundos, pois o intervalo é de 30 segundos.
<p align="center">
  <img src="/Icons and Images/HalfIntervalBeep.gif">
</p>

Ainda há muitas validações que o sistema faz e que não foram mostradas. Mas eventualmente serão conforme a utilização do programa pelo usuário.

Abaixo um vídeo do YouTube demonstrando o funcionamento do software (ainda na versão 1.1.2):
[![YouTube](http://i.ytimg.com/vi/APAvHpSrE9c/hqdefault.jpg)](https://www.youtube.com/watch?v=APAvHpSrE9c)

## Licença

Este é um software livre sob licença GPL 3.0, podendo ser utilizado e modificado livremente.

## Autores e Reconhecimentos

Desenvolvido por Ademilson Rodrigo Moreira baseando-se na ideia inicial de Leandro Medeiros de Almeida.

O logotipo e ícone do programa foi gerado por IA através do [Copilot Designer](https://copilot.microsoft.com/images/create).

Os ícones dos botões são do site [Icons8](https://icons8.com/icon/set/forms-audio/fluency).

## Histórico de Versões

**Versão 1.2.0**
- Adicionado elemento visual como contador regressivo de 3 segundos para começar a prova. Isto servirá para contornar o problema de congelamento da tela ao iniciar a prova com músicas e beeps ao mesmo tempo, pois os ajustes da versão 1.0.0 não corrigiram o problema por completo.

**Versão 1.1.4**
- Resolvido bug em que o formulário liberado após alguma operação de leitura ou escrita de arquivo pelo menu Configuração mantinha o botão de iniciar a competição bloqueado.

**Versão 1.1.3**
- Adicionado texto de dica para os botões "Beep contagem" e "Beep de início";
- Melhoria nas funções de salvar e ler um arquivo de configuração e recarregar lista de beeps. Agora o formulário é bloqueado enquanto o processo está ocorrendo, sendo liberado ao término do processo.

**Versão 1.1.2**
- Melhoria em verificações internas após alterações na versão 1.0.0 devido ao bug de congelamento de tela.

**Versão 1.1.1**
- Resolução de bug que não zerava corretamente o cronômetro utilizado pelo teste dos beeps;
- Ajustado timer interno (não visível e acessível ao usuário) que dispara os beeps alguns milissegundos antes do previsto.

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