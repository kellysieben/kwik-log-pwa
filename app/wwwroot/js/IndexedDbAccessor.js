//////
// another set of calls for indexedDB API
//
export function init_v2() {
    const request = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);

    request.onupgradeneeded = function (event) {
        const db = event.target.result;
        if (!db.objectStoreNames.contains(LOG_COLLECTION_NAME)) {
            db.createObjectStore(LOG_COLLECTION_NAME, { keyPath: 'id', autoIncrement: true });
        }
    };

    request.onerror = function (event) {
        console.error('Failed to create collection', event.target.error);
    };

    request.onsuccess = function (event) {
        console.log(`Successfully created collection ${LOG_COLLECTION_NAME}`);
    };
}

export function getAll_v2(collectionName) {
    const request = indexedDB.open(DATABASE_NAME);

    request.onerror = function (event) {
        console.error('Failed to get all records', event.target.error);
    };

    request.onsuccess = function (event) {
        const db = event.target.result;
        const transaction = db.transaction(collectionName, 'readonly');
        const collection = transaction.objectStore(collectionName);
        const getAllRequest = collection.getAll();

        getAllRequest.onsuccess = function (event) {
            console.log('All records:', event.target.result);
        };
    };
}

//////
// from an article posting: https://blazorschool.com/tutorial/blazor-wasm/dotnet6/indexeddb-storage-749869
//
export function initialize() {
    console.log("JBF.js : initialize.0");
    let kwikIndexedDB = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    kwikIndexedDB.onupgradeneeded = function () {
        console.log("JBF.js : initialize.1.create");
        let db = kwikIndexedDB.result;
        //db.createObjectStore(LOG_COLLECTION_NAME, { keyPath: "id" });
        db.createObjectStore(LOG_COLLECTION_NAME, { autoIncrement: true });
    }
}

export function set(collectionName, value) {
    console.log("JBF.js : set");
    let kwikIndexedDB = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);

    kwikIndexedDB.onsuccess = function () {
        let transaction = kwikIndexedDB.result.transaction(collectionName, "readwrite");
        let collection = transaction.objectStore(collectionName)
        collection.put(value);
    }
}

export async function get(collectionName, id) {
    console.log("JBF.js : get");
    let request = new Promise((resolve) => {
        let kwikIndexedDB = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        kwikIndexedDB.onsuccess = function () {
            let transaction = kwikIndexedDB.result.transaction(collectionName, "readonly");
            let collection = transaction.objectStore(collectionName);
            let result = collection.get(id);

            result.onsuccess = function (e) {
                resolve(result.result);
            }
        }
    });

    let result = await request;

    return result;
}

export async function getAll(collectionName) {
    console.log("JBF.js : getAll");
    let request = new Promise((resolve) => {
        let kwikIndexedDB = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        kwikIndexedDB.onsuccess = function () {
            let transaction = kwikIndexedDB.result.transaction(collectionName, "readonly");
            let collection = transaction.objectStore(collectionName);
            let result = collection.getAll();

            result.onsuccess = function (e) {
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
