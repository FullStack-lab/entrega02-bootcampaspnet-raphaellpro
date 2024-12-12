[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/ycOXvOtz)
# Documento de Requisitos do Sistema de Comentários e Respostas em Fóruns
Esta documentação detalha os requisitos para o desenvolvimento de um Sistema de Comentários e Respostas em Fóruns. Este projeto faz parte do bootcamp de ASP.NET MVC 5 com .NET Framework 4.8 e será desenvolvido como uma aplicação web. O objetivo é consolidar os conhecimentos adquiridos nos módulos já estudados:

 - Introdução ao ASP.NET MVC
 - Roteamento no ASP.NET MVC
 - HTML Helpers no ASP.NET MVC

   
## Objetivo
O objetivo do Sistema de Comentários e Respostas em Fóruns é criar uma aplicação que permita aos usuários interagirem em um ambiente colaborativo por meio de comentários e respostas, aplicando os fundamentos do ASP.NET MVC para estruturar uma aplicação web funcional e escalável.

## Descrição
O Sistema de Comentários e Respostas será uma aplicação web estruturada em ASP.NET MVC que permitirá aos usuários:

 - Visualizar comentários e suas respectivas respostas em uma estrutura hierárquica.
 - Criar novos comentários em tópicos do fórum.
 - Responder a comentários já existentes.
 - Editar ou excluir seus próprios comentários e respostas.
 - Visualizar detalhes completos de um comentário, incluindo todas as suas respostas.
   
O sistema deve refletir as práticas aprendidas nos módulos anteriores, com foco na aplicação prática e no aprendizado.

## Requisitos do Projeto

### Funcionalidades Principais

#### 1. Listagem de Comentários:

Exibir uma lista de comentários principais (tópicos) cada um com suas respostas aninhadas e com as seguintes informações no corpo do mesmo:

 - Texto do comentário.
 - Autor do comentário.
 - Data/hora de criação.
 - Número de respostas (se houver).
   
#### 2. Criação de Comentário:

Formulário para criação de um comentário principal, com os campos:

 - Texto do comentário (máximo de 500 caracteres).
 - Nome do autor.

#### 3. Responder a Comentários:

 - Opção para responder a um comentário existente.
 - Formulário semelhante ao de criação de comentário.
   
#### 4. Edição de Comentários e Respostas:

 - Possibilidade de editar comentários ou respostas criados pelo próprio usuário.
 - Apenas o texto do comentário pode ser editado.

#### 5. Exclusão de Comentários e Respostas:

 - Permitir que o autor exclua seus comentários ou respostas.
 - Comentários excluídos também excluem suas respostas associadas.
   
#### 6. Visualização Detalhada:

 - Página com os detalhes completos de um comentário principal e suas respostas.
   
## Regras e Restrições

 - Armazenamento Temporário: Para esta entrega, os dados podem ser armazenados em uma lista estática (banco de dados é opcional, visto que é um conteúdo ministrado de forma adiantada).
 - Validação: Utilize as validações do ASP.NET MVC para garantir a integridade dos dados, como campos obrigatórios e limites de texto.
 - Roteamento: Configure o roteamento para que cada funcionalidade seja acessível por URLs amigáveis.
 - HTML Helpers: Utilize HTML Helpers para construir as Views.
 - Interface: Garanta uma navegação intuitiva e um design simples para usuários.
   
## Páginas Necessárias

### 1. Listagem de Comentários (Página Principal):

 - Exibe os comentários principais e um botão para adicionar novo comentário.
 - Botões para visualizar detalhes, editar ou excluir cada comentário.

### 2. Criação/Edição de Comentários:

 - Formulário para criar ou editar comentários.

### 3. Detalhes de Comentário:

 - Exibe o comentário selecionado com todas as suas respostas.
 - Botão para adicionar uma nova resposta.
   
### 4. Criação de Resposta:

 - Formulário para adicionar uma resposta a um comentário.

   
## Dicas
 - Reaproveite o conhecimento aplicado nos projetos dos módulos anterior para estruturar as funcionalidades.
 - Use Razor e HTML Helpers para criar as Views.
 - Planeje o sistema pensando na escalabilidade, pois melhorias podem ser solicitadas em próximas entregas.
 - Evite adicionar funcionalidades além do solicitado.
 - Procure cumprir tudo o que está sendo solicitado na documentação.
   
## Checklist 📝

 - Abaixo temos uma checklist de requisitos/funcionalidades que vocês deven cumprir, usem isso para auxiliar na organização do passo a passo de desenvolvimento desse sistema

|     | Descrição                  |
| --- | -------------------------- |
| [ ] | Listar comentários principais e suas respostas          |  
| [ ] | Criar um novo comentário |
| [ ] | Responder a um comentário existente.    |    
| [ ] | Editar comentários ou respostas.      | 
| [ ] | Excluir comentários ou respostas. |
| [ ] | Vizualizar detalhes de um comentário com respostas |

## Entrega
 - Prazo: O repositório estará aberto para commits até o dia 16/12 às 12:00
 - Revisão: Certifique-se de testar todas as funcionalidades antes da entrega.
 - Commits: Realize commits frequentes, com mensagens claras e descritivas.

Boa sorte à todos e bom aprendizado! Abraços, Fernando Zuchi.
