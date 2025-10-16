# 🐳 Building and Running the Docker Image

This guide explains how to build and run the Docker image for the project.

---

## 🧩 Prerequisites

Before you begin, make sure you have the following installed:

- [Docker](https://www.docker.com/get-started) (version 20.x or later)
- Terminal or command prompt access

---

## ⚙️ Build Instructions

1. **Open your terminal**  
   Navigate to your project’s working directory:

   ```bash
   cd /path/to/your/project
   ```

2. **Build the Docker image**  
   Use the following command to build the image:

   ```bash
   docker build -t pametnomesto:latest .
   ```

   **Explanation:**
   - `-t pametnomesto:latest` assigns a name (`pametnomesto`) and tag (`latest`) to the image.  
   - The `.` indicates the current directory as the build context.

---

## 🚀 Running the Container

Once the image has been built successfully, you can run it using:

```bash
docker run -it --rm pametnomesto:latest
```

**Options explained:**
- `-it` runs the container in interactive mode with a terminal attached.  
- `--rm` automatically removes the container when it exits.  
- `pametnomesto:latest` is the image name and tag.

If your project exposes a specific port (for example, `8080`), you can map it with:

```bash
docker run -it --rm -p 8080:8080 pametnomesto:latest
```

Then visit: [http://localhost:8080](http://localhost:8080)

---

## 🧹 Cleaning Up

To remove the image from your system:

```bash
docker rmi pametnomesto:latest
```

---

## 🧾 Notes

- Update the tag (`latest`) as needed for versioning.
- Modify port mappings and environment variables according to your project’s configuration.

---

**Author:**  _Aljaž Lackovič, Filip Novak_
**Project:** _Pametno Mesto_
