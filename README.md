# Bulk PDF Compressor & Structure Mirror
This **C# .NET** utility is designed for high-volume PDF optimization. It automates the compression of hundreds of files across nested directories while ensuring the **original folder hierarchy** is perfectly recreated in the output destination.

## Key Features
*   **High-Volume Bulk Processing:** Optimized to handle large-scale document archives in a single run.
*   **Zero-Quality-Loss Compression:** Shrinks file sizes by stripping redundant metadata and optimizing internal objects without degrading visual fidelity.
*   **Recursive Folder Mirroring:** Automatically scans all sub-directories and maps the exact folder structure in the new 'Compressed' root folder.
*   **Fault-Tolerant Processing:** Built-in error handling to skip corrupt files and continue the bulk queue without interruption.

## Mirroring Logic Example
- **Source:** `C:\Archives\Invoices\2024\Tax_Return.pdf`
- **Output:** `D:\Compressed_Output\Invoices\2024\Tax_Return.pdf` (Optimized)

## Tech Stack
- **Framework:** .NET 6.0 / .NET Core
- **Language:** C#
- **IO Strategy:** Recursive Directory Traversal
