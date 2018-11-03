# Home automation with Raspberry Pi

## Protocol

* Using little endian
* Using unicode for string

Request Types:
1. connection 01
  1. connect 00
1. rooms 02
  1. create 00

request
> request type (2 byte) counter (ushort) data length (uint) data (data length bytes) crc (2 byte)
```
sample (01 00 00 00 00 00 00 00 C1 CC)
```
response
> request type (2 byte) counter (ushort) response code (2 byte) data length (uint) data (data length bytes) crc (2 byte)
```
sample (01 00 00 00 00 00 04 00 00 00 AA BB CC DD C5 96)
```

* connect request
```
01 00 00 00 00 00 00 00 C1 CC
```
* connect response
```
01 00 00 00 04 00 00 00 | AA BB CC DD | 38 94
```
* create room request (living room)
```
02 00 01 00 16 00 00 00 | 6C 00 69 00 76 00 69 00 6E 00 67 00 20 00 72 00 6F 00 6F 00 6D 00 | 4E CE
```
* create room respone
```
02 00 01 00 00 00 00 00 00 00 60 06
```
repsonse codes 
created: 00 00
duplicate room name 01 00
