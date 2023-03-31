//////
// from an article posting: https://blazorschool.com/tutorial/blazor-wasm/dotnet6/indexeddb-storage-749869
//
export function initialize() {
    let kwikIndexedDB = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    kwikIndexedDB.onupgradeneeded = function () {
        let db = kwikIndexedDB.result;
        //db.createObjectStore(LOG_COLLECTION_NAME, { keyPath: "id" });
        db.createObjectStore(LOG_COLLECTION_NAME, { autoIncrement: true });
    }
}

export function set(collectionName, value) {
    let kwikIndexedDB = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);

    kwikIndexedDB.onsuccess = function () {
        let transaction = kwikIndexedDB.result.transaction(collectionName, "readwrite");
        let collection = transaction.objectStore(collectionName)
        collection.put(value);
    }
}

export async function get(collectionName, id) {
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

export async function getAllByOwner(collectionName, ownerId) {
    const request = new Promise((resolve) => {
        const dbReq = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);

        dbReq.onerror = event => {
            console.error('Database error:', event.target.errorCode);
        };
        dbReq.onsuccess = event => {
            const db = event.target.result;
            const transaction = db.transaction(LOG_COLLECTION_NAME, 'readonly');
            const store = transaction.objectStore(LOG_COLLECTION_NAME);
            const records = [];

            const cursorRequest = store.openCursor();
            cursorRequest.onerror = event => {
                console.error('Cursor error:', event.target.errorCode);
            };
            cursorRequest.onsuccess = event => {
                const cursor = event.target.result;
                if (cursor) {
                    if (cursor.value.ownerId == ownerId) {
                        records.push(cursor.value);
                    }
                    cursor.continue();
                }
                else {
                    resolve(records);
                }
            };
        };
    });

    let result = await request;

    return result;
}

export async function getAllByOwner_V1_does_not_work(collectionName, ownerId) {
    const kwikIndexedDB = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    const transaction = kwikIndexedDB.transaction([collectionName], "readonly");
    const objectStore = transaction.objectStore(collectionName);
    const rvals = [];

    objectStore.openCursor().onsuccess = (event) => {
        const cursor = event.target.result;
        if (cursor) {
            if (cursor.value.ownerId == ownerId) {
                rvals.appendChild(cursor.value);
            }
            cursor.continue();
        }
    };

    return rvals;
}

let CURRENT_VERSION = 1;
let DATABASE_NAME = "kwik_log";
let LOG_COLLECTION_NAME = "entries";
