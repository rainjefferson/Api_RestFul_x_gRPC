# Api_RestFul_x_gRPC
Comparando APIs RESTFul e gRPC

## API Restful (HTTP/1.1)
Funcionamento: Utiliza o protocolo HTTP/1.1 e geralmente retorna dados em formato JSON ou XML.

#### Vantagens:
Fácil de implementar e consumir.
Amplamente adotado e compatível com a maioria dos clientes.

#### Desvantagens:
Overhead maior devido ao uso de texto (JSON/XML).
Menos eficiente em termos de desempenho comparado ao gRPC.


## gRPC (HTTP/2)
Funcionamento: Utiliza o protocolo HTTP/2 e o formato binário Protocol Buffers para serialização de dados.

#### Vantagens:
Mais rápido e eficiente devido ao uso de binários e multiplexação do HTTP/2.
Ideal para comunicação entre microserviços.

#### Desvantagens:
Requer mais configuração.
Menos compatível com clientes que não suportam HTTP/2.

## Explicação dos projetos (solução)
- Dois projetos APIs, RESTFul e gRPC e um projeto Console para consumir as APIs.
- Console executa as requisições RESTful e gRPC simultaneamente.
- Faz 1000 requisições para cada API.
- Mede o tempo total gasto e cálculo da média por requisição.
- Exibe a API mais rápida com base na média de tempo.
