// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', () => { });

// function to connect to mongoDB atlas
async function connectToMongoDB() {
    const MongoClient = require('mongodb').MongoClient;
    const uri = process.env.MONGODB_URI;
    const client = new MongoClient(uri, { useNewUrlParser: true });
    await client.connect();
    return client;
}

// function to get the collection from mongoDB atlas
async function getCollection(client) {
    const collection = client.db("test").collection("devices");
    return collection;
}

// function to insert a document into the collection
async function insertDocument(collection, document) {
    const result = await collection.insertOne(document);
    return result;
}

// function to find a document in the collection
async function findDocument(collection, document) {
    const result = await collection.findOne(document);
    return result;
}

// function to update a document in the collection
async function updateDocument(collection, document, update) {
    const result = await collection.updateOne(document, update);
    return result;
}

// function to delete a document from the collection
async function deleteDocument(collection, document) {
    const result = await collection.deleteOne(document);
    return result;
}

// function to close the connection to mongoDB atlas
async function closeConnection(client) {
    await client.close();
}


