# business-management-communication

## WebHook Configuration - Local

### Ngrok

```bash
ngrok -v
ngrok http 5275
```

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