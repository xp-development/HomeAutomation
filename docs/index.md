# Home automation with Raspberry Pi

## Protocol

* Using little endian
* Using unicode for string

request
> request type (2 byte) counter (2 byte) data length (4 byte) data (data length bytes) crc (2 byte)
```
sample (01 00 00 00 00 00 00 00 C1 CC)
```
response
> request type (2 byte) counter (2 byte) data length (4 byte) data (data length bytes) crc (2 byte)
```
sample (01 00 00 00 04 00 00 00 AA BB CC DD 38 94)
```

* connect request
```
01 00 00 00 00 00 00 00 C1 CC
```
* connect response
```
01 00 00 00 04 00 00 00 | AA BB CC DD | 38 94
```
Add room

