# API

## GET /:name

Returns a JSON blob of the whole state of a given automata. 
If the name does not exist, then return a 404

## GET /:name/cell?x=0&y=0

Returns a JSON blob of a given cell of a given automata
If the name does not exist, then return a 404

## PUT /:name?width=16&height=16

Add or overwrite an empty board to the state under the given name

```bash=
curl -X PUT 'localhost:8080/b?width=32&height=32'
```

## PUT /:name/cell?x=0&y=0&v=true

Changes the cell at the given coordinates to the given value
If the name does not exist, then return a 404

```bash=
curl -X PUT 'localhost:8080/b?x=2&y=3&v=true'
```

## POST /:name/next

Update the board with the given name 
We used POST because PUT is usually idempotent

```bash=
curl -X POST 'localhost:8080/b/next'
```
