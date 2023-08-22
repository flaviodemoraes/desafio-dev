# Usa a imagem Node.js como base
FROM node:19.5.0-alpine AS builder

# Define o diretório de trabalho
WORKDIR /app

# Copia o arquivo package.json e package-lock.json
COPY ["front-end/package.json", "front-end/package-lock.json", "./"]
RUN npm install

# Copia os arquivos do projeto
COPY front-end/. .

# Build da aplicação Angular
RUN npm run build --prod

# Segunda fase do Dockerfile
FROM nginx:alpine AS final
COPY --from=builder /app/dist/front-end /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
