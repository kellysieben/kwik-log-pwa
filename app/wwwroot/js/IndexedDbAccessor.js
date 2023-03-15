export function initialize()
{
    console.log("JBF.js : initialize.0");
    let kwikIndexedDB = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    kwikIndexedDB.onupgradeneeded = function ()
    {
        console.log("JBF.js : initialize.1.create");
        let db = kwikIndexedDB.result;
        //db.createObjectStore(LOG_COLLECTION_NAME, { keyPath: "id" });
        db.createObjectStore(LOG_COLLECTION_NAME, { autoIncrement: true});
    }
}

export function set(collectionName, value)
{
    console.log("JBF.js : set");
    let kwikIndexedDB = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);

    kwikIndexedDB.onsuccess = function ()
    {
        let transaction = kwikIndexedDB.result.transaction(collectionName, "readwrite");
        let collection = transaction.objectStore(collectionName)
        collection.put(value);
    }
}

export async function get(collectionName, id)
{
    console.log("JBF.js : get");
    let request = new Promise((resolve) =>
    {
        let kwikIndexedDB = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        kwikIndexedDB.onsuccess = function ()
        {
            let transaction = kwikIndexedDB.result.transaction(collectionName, "readonly");
            let collection = transaction.objectStore(collectionName);
            let result = collection.get(id);

            result.onsuccess = function (e)
            {
                resolve(result.result);
            }
        }
    });

    let result = await request;

    return result;
}

let CURRENT_VERSION = 1;
let DATABASE_NAME = "kwik_log";
let LOG_COLLECTION_NAME = "entries";