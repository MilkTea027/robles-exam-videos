# Stage 1: Build the Angular app
FROM node:20 AS build
WORKDIR /app
COPY . .
RUN npm install && npm run build --prod

# Stage 2: Serve with Nginx
FROM nginx:alpine AS final
COPY --from=build /app/dist/ui/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf