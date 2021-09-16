#Chińczyk frontend

###config file
config file must be send just after game startup  


`{
    player_id: str,
    player_nick: str,
    room_id: str,
    server_address: str
}`

`unityInstance.SendMessage('GameController','ConfigFromJson', jsonStringParams)`