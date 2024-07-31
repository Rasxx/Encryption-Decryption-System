# Encryption-Decryption-System

The application is designed to encrypt and decrypt messages provided by the user, along with the necessary key, in order to ensure data integrity and confidentiality.

 The program was developed using the C# programming language.
 
## Table of Contents
- [Features](#features)
- [Encryption Block Diagram ](#encryption-block-diagram)
- [Decryption Block Diagram ](#decryption-block-diagram)
- [Key Block Diagram  ](#key-block-diagram)


## Features
- Allows users to encrypt and decrypt a text data.

## Encryption Block Diagram 
- The plaintext is split into two equal halves 
- Then each half will be shifted left by two .
- Then combine the two Parts .
- Then XORed them with encryption Key that is inserted by the user to obtain the ciphertext.

## Decryption Block Diagram 
- The ciphertext is XORed with the encryption key .
- Then the decrypted plaintext is split into two equal halves.
- Each half of the decrypted plaintext is shifted right by two.
- Then the shifted halves are combined to obtain the original plaintext.

## Key Block Diagram  
- The Key will be shifted left by two.
- Then passes through P-Box transformation to genertate the new key.

