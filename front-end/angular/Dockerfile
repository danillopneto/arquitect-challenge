FROM node:14.15.1 as build
WORKDIR /app

RUN npm install -g @angular/cli

COPY ./package.json .
RUN npm install
COPY . .
RUN ng build --prod

FROM nginx as runtime
COPY --from=build /app/dist /usr/share/nginx/html