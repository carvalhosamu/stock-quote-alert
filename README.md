## stock-quote-alert
Worker Service Para realizar Cotações de ações brasileiras


# Formas de Envio de email
SEMPRE: toda vez que o serviço der um pooling na api de cotação 

DIFERENCAVALOR : será enviado um email seguindo a regra (valorUltimoEnvioEmail - diferenca < valorCotacao) para valores menores que o valor mínimo
e (valorUltimoEnvioEmail + diferenca > valorCotacao) para valores maiores que o valor máximo informado (valor da diferença é informado no appsettings.json)

UMAVEZ: Sera enviado email apenas uma vez quando houver um aumento e depois seria enviado novamente somente quando o valor da cotação ficar abaixo do valor mínimo.

# Bibliotecas Utilizadas
EntityFramework: Biblioteca para persistencia de dados em base SQLite 
Newtonsoft.Json: Biblioteca de manipulação de objetos JSON 
RestSharp: Biblioteca para realizar requisições e enviar informações para API'S
FluentMail: Biblioteca para envio de emails

# Template 
Foi utilizado um template disponível no site https://www.benchmarkemail.com/email-templates/outdoors-email-template/ para o email de envio

# Versão .NET 5.0
