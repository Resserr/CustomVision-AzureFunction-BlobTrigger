FROM python:3.7-windowsservercore-1809

RUN Invoke-WebRequest 'https://download.microsoft.com/download/6/D/F/6DF3FF94-F7F9-4F0B-838C-A328D1A7D0EE/vc_redist.x64.exe' -OutFile vcredist_x64.exe ; \
    Start-Process vcredist_x64.exe -ArgumentList '/install', '/passive', '/norestart' -NoNewWindow -Wait ; \
    Remove-Item vcredist_x64.exe

RUN pip install --no-cache-dir numpy~=1.17.5 tensorflow~=2.0.2 flask~=1.1.2 pillow~=6.1.0
RUN pip install --no-cache-dir mscviplib==2.0.130828

# Set the working directory
COPY app /app
WORKDIR /app

# Expose the port
EXPOSE 80

# Run the flask server for the endpoints
CMD python -u app.py