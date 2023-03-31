// https://www.mongodb.com/docs/mongodb-vscode/playgrounds/
use('kwik_log');

db.getCollection('entries').insertMany([
  { 'Entry': 'paranoid android', 'oid': '21035be2-384d-4002-a1f6-d899d42ecb2d', 'TimestampUTC': new Date('2023-03-29T08:00:00Z') },
  { 'Entry': 'karma police', 'oid': '21035be2-384d-4002-a1f6-d899d42ecb2d', 'TimestampUTC': new Date('2023-03-31T12:31:09Z') },
  { 'Entry': 'fake plastics trees', 'oid': '7a30a98c-8f14-4d55-8861-11ddccaa303d', 'TimestampUTC': new Date('2023-03-30T21:59:59Z') },
]);
