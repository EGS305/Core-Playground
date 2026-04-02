class GX {
    Init() {
        const canvas = document.getElementById('3dPanel');
        const gl = this.gl = canvas.getContext('webgl2');
        if (!gl) throw new Error('WebGL not supported');
        gl.enable(gl.CULL_FACE);

        const program = gl.createProgram();
        gl.attachShader(program, gx.LoadShader(gl.VERTEX_SHADER, this.VertexShader));
        gl.attachShader(program, gx.LoadShader(gl.FRAGMENT_SHADER, this.FragmentShader));
        gl.linkProgram(program);
        if (!gl.getProgramParameter(program, gl.LINK_STATUS)) throw new Error('Shader program failed to link: ' + gl.getProgramInfoLog(program));
        gl.useProgram(program);

        const projection = glMatrix.mat4.create();
        glMatrix.mat4.perspective(projection, Math.PI / 4, canvas.width / canvas.height, 0.1, 100);
        gl.uniformMatrix4fv(gl.getUniformLocation(program, 'uProjection'), false, projection);

        const camera = glMatrix.mat4.create();
        glMatrix.mat4.lookAt(camera, [-20, 15, 20], [0, 0, 0], [0, 1, 0]);
        gl.uniformMatrix4fv(gl.getUniformLocation(program, 'uCamera'), false, camera);

        const buffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, buffer);

        gl.vertexAttribPointer(0, 3, gl.FLOAT, false, 36, 0);
        gl.vertexAttribPointer(1, 3, gl.FLOAT, false, 36, 12);
        gl.vertexAttribPointer(2, 3, gl.FLOAT, false, 36, 24);
        gl.enableVertexAttribArray(0);
        gl.enableVertexAttribArray(1);
        gl.enableVertexAttribArray(2);
        console.log('3D initialized');
    }

    LoadShader(type, source) {
        const shader = this.gl.createShader(type);
        this.gl.shaderSource(shader, source);
        this.gl.compileShader(shader);

        if (!this.gl.getShaderParameter(shader, this.gl.COMPILE_STATUS)) {
            const info = this.gl.getShaderInfoLog(shader);
            this.gl.deleteShader(shader);
            throw new Error('Shader compilation failed: ' + info);
        }

        return shader;
    }

    Update(objects) {
        const data = [];
        let nrTriangles = 0;

        if (objects && objects.length > 0) {
            objects.forEach(obj => {
                let normal = [0, 0, 1];
                this.Append(data, obj, -1, 1, 1, normal);
                this.Append(data, obj, -1, -1, 1, normal);
                this.Append(data, obj, 1, -1, 1, normal);
                this.Append(data, obj, 1, -1, 1, normal);
                this.Append(data, obj, 1, 1, 1, normal);
                this.Append(data, obj, -1, 1, 1, normal);
                nrTriangles += 6; //front

                normal = [0, 0, -1];
                this.Append(data, obj, 1, 1, -1, normal);
                this.Append(data, obj, 1, -1, -1, normal);
                this.Append(data, obj, -1, -1, -1, normal);
                this.Append(data, obj, -1, -1, -1, normal);
                this.Append(data, obj, -1, 1, -1, normal);
                this.Append(data, obj, 1, 1, -1, normal);
                nrTriangles += 6; //back

                normal = [1, 0, 0];
                this.Append(data, obj, -1, 1, -1, normal);
                this.Append(data, obj, -1, -1, -1, normal);
                this.Append(data, obj, -1, -1, 1, normal);
                this.Append(data, obj, -1, -1, 1, normal);
                this.Append(data, obj, -1, 1, 1, normal);
                this.Append(data, obj, -1, 1, -1, normal);
                nrTriangles += 6; //left

                normal = [-1, 0, 0];
                this.Append(data, obj, 1, 1, -1, normal);
                this.Append(data, obj, 1, 1, 1, normal);
                this.Append(data, obj, 1, -1, 1, normal);
                this.Append(data, obj, 1, -1, 1, normal);
                this.Append(data, obj, 1, -1, -1, normal);
                this.Append(data, obj, 1, 1, -1, normal);
                nrTriangles += 6; //right

                normal = [0, 1, 0];
                this.Append(data, obj, -1, 1, -1, normal);
                this.Append(data, obj, -1, 1, 1, normal);
                this.Append(data, obj, 1, 1, 1, normal);
                this.Append(data, obj, 1, 1, 1, normal);
                this.Append(data, obj, 1, 1, -1, normal);
                this.Append(data, obj, -1, 1, -1, normal);
                nrTriangles += 6; //top

                normal = [0, -1, 0];
                this.Append(data, obj, -1, -1, 1, normal);
                this.Append(data, obj, -1, -1, -1, normal);
                this.Append(data, obj, 1, -1, -1, normal);
                this.Append(data, obj, 1, -1, -1, normal);
                this.Append(data, obj, 1, -1, 1, normal);
                this.Append(data, obj, -1, -1, 1, normal);
                nrTriangles += 6; //bottom
            });
        }

        this.gl.bufferData(this.gl.ARRAY_BUFFER, new Float32Array(data), this.gl.STATIC_DRAW);
        this.gl.clearColor(0.1, 0.1, 0.1, 1);
        this.gl.clear(this.gl.COLOR_BUFFER_BIT);
        this.gl.drawArrays(this.gl.TRIANGLES, 0, nrTriangles);
    }

    Append(data, obj, x, y, z, normal) {
        data.push(obj.position.x + obj.size.x * x, obj.position.y + obj.size.y * y, obj.position.z + obj.size.z * z);
        data.push(obj.color.x, obj.color.y, obj.color.z);
        data.push(...normal);
    }

    VertexShader = `#version 300 es
    layout(location = 0) in vec3 aPos;
    layout(location = 1) in vec3 aCol;
    layout(location = 2) in vec3 aNor;
    uniform mat4 uProjection;
    uniform mat4 uCamera;
    out vec3 oCol;
    out vec3 oNor;

    void main() {
        gl_Position = uProjection * uCamera * vec4(aPos, 1);
        oCol = aCol;
        oNor = aNor;
    }`;

    FragmentShader = `#version 300 es
    precision lowp float;
    in vec3 oCol;
    in vec3 oNor;
    out vec4 fragColor;
    const vec3 LIGHT_POS = normalize(vec3(-2, 1.5, 2));

    void main() {
        float diffuse = (dot(oNor, normalize(LIGHT_POS)) + 1.0) / 2.0;
        fragColor = vec4(oCol * diffuse, 1);
    }`;
}

window.gx = new GX();