# business-management-communication

## WebHook Configuration - Local

### Ngrok

```bash
ngrok -v
ngrok http 5275
```

### AWS deploy config

sudo yum update -y
sudo dnf install dotnet-sdk-8.0 -y
sudo yum install docker git -y
sudo systemctl start docker
sudo systemctl enable docker

sudo curl -SL https://github.com/docker/compose/releases/download/v2.39.2/docker-compose-linux-x86_64 -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose

git clone https://github.com/Tavo826/business-management-communication

nano .env

sudo docker-compose up -d --build

rm -rf business-management-persistence

mkdir -p certbot/www
mkdir -p certbot/conf

mkdir -p nginx/conf.d
nano nginx/conf.d/default.conf


**Nginx config - ./nginx/conf.d/default.conf**

´´´
server {
    listen 80;
    server_name customermanagement.top;

    location /.well-known/acme-challenge/ {
        root /var/www/certbot;
    }

    location / {
        return 301 https://$host$request_uri;
    }
}

server {
    listen 443 ssl;
    server_name customermanagement.top;

    ssl_certificate /etc/letsencrypt/live/customermanagement.top/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/customermanagement.top/privkey.pem;
    include /etc/letsencrypt/options-ssl-nginx.conf;
    ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem;

    location / {
        proxy_pass http://business-communication:8080;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}
´´´

docker-compose up -d business-communication nginx

docker run --rm -it -v $(pwd)/certbot/www:/var/www/certbot -v $(pwd)/certbot/conf:/etc/letsencrypt certbot/certbot certonly --webroot --webroot-path=/var/www/certbot --email 9gagigor816@gmail.com --agree-tos --no-eff-email -d customermanagement.top

docker-compose restart nginx



### Endpoints 

1. **Name:** Webhook  
**Description:** Webhook checking for meta  
**Endpoint:** /api/v1/Message/webhook  
**Type:** GET
- **Request**
    - **Params**
        - **Name:** hub.mode  
            **Type:** Query  
            **Example:** subscribe
        - **Name:** hub.challenge  
            **Type:** Query  
            **Example:** 977639611
        - **Name:** hub.verify_token  
            **Type:** Query  
            **Example:** b1c66e2a-0a4e-4176-b646-e21ef792ad60
- **Response:**
    -  **Status code:** 200  
        **Body:**
        ```
        977639611
        ```

2. **Name:** Receive message  
**Description:** Webhook to get the message from user   
**Endpoint:** /api/v1/Message/webhook  
**Type:** POST
- **Request**
    - **Body:**
        ```json
        {
            "object": "whatsapp_business_account",
            "entry": [
                {
                    "id": "8856996819413533",
                    "changes": [
                        {
                            "value": {
                                "messaging_product": "whatsapp",
                                "metadata": {
                                    "display_phone_number": "16505553333",
                                    "phone_number_id": "27681414235104944"
                                },
                                "contacts": [
                                    {
                                        "profile": {
                                            "name": "Kerry Fisher"
                                        },
                                        "wa_id": "16315551234"
                                    }
                                ],
                                "messages": [
                                    {
                                        "from": "16315551234",
                                        "id": "wamid.ABGGFlCGg0cvAgo-sJQh43L5Pe4W",
                                        "timestamp": "1603059201",
                                        "text": {
                                            "body": "Hello this is an answer"
                                        },
                                        "type": "text"
                                    }
                                ]
                            },
                            "field": "messages"
                        }
                    ]
                }
            ]
        }
        ```
- **Response:**
    - **Status code:** 200  
        **Body:**
        ```json
        
        ```

3. **Name:** Send message  
**Description:** Send message to user   
**Endpoint:** /api/v1/Message/SendMessage  
**Type:** POST
- **Request**
    - **Body:**
        ```json
        {
            "senderPhone": "573217637663",
            "senderPhoneId": "757269984128270",
            "textMessage": "how are you?"
        }
        ```
- **Response:**
    - **Status code:** 200
        **Body:**
        ```json
        
        ```