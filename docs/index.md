# Home automation with Raspberry Pi

## Protocol

* Using little endian
* Using unicode for string
* Server connection timeout 5 min
* CRC checksum for all samples: AF FE
* Connection identifier for all samples: CC CC CC CC
* Protocol version for all commands: 00

### Request Types:
1. connection 01
  - connect 00
1. rooms 02
  - create 00
  - get all 01
  - get description 02

### Common response codes:

1. wrong crc FF 01
2. not connected FF 02

#### Commands (no connect required)

request
> protocol version (1 byte) request type (2 byte) counter (ushort) data length (uint) data (data length bytes) crc (2 byte)

```
sample (00 01 00 00 00 00 00 00 00 AF FE)
```
response
> protocol version (1 byte) request type (2 byte) counter (ushort) response code (2 byte) data length (uint) data (data length bytes) crc (2 byte)

```
sample (00 01 00 00 00 00 00 04 00 00 00 AA BB CC DD AF FE)
```

#### Commands (connect required)

request
> protocol version (1 byte) request type (2 byte) counter (ushort) connection identifier (ushort) data length (uint) data (data length bytes) crc (2 byte)

```
sample (00 02 01 01 00 CC CC CC CC 00 00 00 00 AF FE)
```
response
> protocol version (1 byte) request type (2 byte) counter (ushort) connection identifier (ushort) response code (2 byte) data length (uint) data (data length bytes) crc (2 byte)

```
sample (00 02 01 01 00 CC CC CC CC 00 00 16 00 00 00 | AA BB CC DD 11 00 22 33 44 55 66 77 99 88 77 66 | AF FE)
```

### Connection
#### connect

> request

```
00 01 00 00 00 00 00 00 00 AF FE
```

> response (data: connection identifier)

```
00 01 00 00 00 04 00 00 00 | CC CC CC CC | AF FE
```

> repsonse codes

- success: 00 00

### Rooms
#### create room

> request (data: living room)

```
00 02 00 01 00 CC CC CC CC 16 00 00 00 | 6C 00 69 00 76 00 69 00 6E 00 67 00 20 00 72 00 6F 00 6F 00 6D 00 | AF FE
```

> respone (data: unique room identifier if response code is 00 00)

```
00 02 00 01 00 CC CC CC CC 00 00 04 00 00 00 | AA BB CC DD | AF FE
```

> repsonse codes

- success: 00 00 
- duplicate room name 01 00

#### get all rooms

> request

```
00 02 01 01 00 CC CC CC CC 00 00 00 00 AF FE
```

> respone (data: gets 4 unique identifier, for each room one identifier if response code is 00 00)

```
00 02 01 01 00 CC CC CC CC 00 00 16 00 00 00 | AA BB CC DD 11 00 22 33 44 55 66 77 99 88 77 66 | AF FE
```

> repsonse codes

- created: 00 00 

#### get room description

> request

```
00 02 02 01 00 CC CC CC CC 04 00 00 00 | AA BB CC DD | AF FE
```

> respone (data: get decription if response code is 00 00)

```
00 02 02 01 00 CC CC CC CC 00 00 16 00 00 00 | 6C 00 69 00 76 00 69 00 6E 00 67 00 20 00 72 00 6F 00 6F 00 6D 00 | AF FE
```

> repsonse codes

- success: 00 00 
- unknown room identifier: 01 00 
