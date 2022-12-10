import cv2
import mediapipe as mp
import socket

mp_hands=mp.solutions.hands
hands=mp_hands.Hands(static_image_mode=False,
                     max_num_hands=2,
                     min_detection_confidence=0.7,
                     min_tracking_confidence=0.5)
mpDraw=mp.solutions.drawing_utils

# 使用udp进行数据传输
sock=socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
serverAddressPort=('127.0.0.1',9090)

# 单帧处理
def process_frame(img):
    img=cv2.flip(img,1)
    img_RGB=cv2.cvtColor(img,cv2.COLOR_BGR2RGB)
    results=hands.process(img_RGB)

    h,w=img.shape[0],img.shape[1]

    # 按照索引下标进行传输
    sendList=[]

    # 如果有检测到手
    if results.multi_hand_landmarks:
        # 遍历每一只检测出的手
        # mpDraw.draw_landmarks(img, hand_21, mp_hands.HAND_CONNECTIONS)  # 可视化
        # 这里用自定义获取原始数据的方法进行可视化
        handness_str=''
        index_finger_tip_str=''
        for hand_idx in range(len(results.multi_hand_landmarks)):
            # 获取该手21个关键点坐标
            hand_21=results.multi_hand_landmarks[hand_idx]
            #可视化关键点及骨架链接
            mpDraw.draw_landmarks(img,hand_21,mp_hands.HAND_CONNECTIONS)

            # 记录左右手信息
            temp_handness=results.multi_handedness[hand_idx].classification[0].label
            handness_str+='{}:{}'.format(hand_idx,temp_handness)

            # 获取手腕的根部深度坐标，mediapipe规定手腕根部是可以看作近似的原点坐标
            cz0=hand_21.landmark[0].z

            # 遍历该手的21个关键点
            for i in range(21):
                # 获取三维x,y,z坐标
                cx=int(hand_21.landmark[i].x*w)
                cy=int(hand_21.landmark[i].y*h)
                cz=hand_21.landmark[i].z

                # 这里把cx、cy、cz传输过去
                sendList.append([cx,h-cy,cz])

                # 这里深度的缩放系数设置为了h
                depth_z=(cz0-cz)

                radius=int(6*(1+depth_z))

                # 手腕
                if i==0:
                    img=cv2.circle(img,(cx,cy),radius*2,(0,0,255),-1)
                # 食指指尖
                if i==8:
                    img=cv2.circle(img,(cx,cy),radius*2,(193,182,255),-1)
                    index_finger_tip_str += '{}:{:.2f} '.format(hand_idx, depth_z)
                if i in [1, 5, 9, 13, 17]:  # 指根
                    img = cv2.circle(img, (cx, cy), radius, (16, 144, 247), -1)
                if i in [2, 6, 10, 14, 18]:  # 第一指节
                    img = cv2.circle(img, (cx, cy), radius, (1, 240, 255), -1)
                if i in [3, 7, 11, 15, 19]:  # 第二指节
                    img = cv2.circle(img, (cx, cy), radius, (140, 47, 240), -1)
                if i in [4, 12, 16, 20]:  # 指尖（除食指指尖）
                    img = cv2.circle(img, (cx, cy), radius, (223, 155, 60), -1)

            sock.sendto(str.encode(str(sendList)), serverAddressPort)
            print(f'发送成功，数据为{str.encode(str(sendList))}')
    return img

# 调用摄像头获取帧数
def use_camera():
    cap=cv2.VideoCapture(0)
    cap.open(1)

    # time.sleep(2)

    while cap.isOpened():
        success,frame=cap.read()
        if not success:
            print('Error')
            break

        # 处理帧
        frame=process_frame(frame)

        # print(frame.size)
        if frame is not None:
            cv2.imshow('hands',frame)

        if cv2.waitKey(1) in [ord('q'),27]:
            break

    cap.release()
    cv2.destroyAllWindows()

if __name__ == '__main__':
    use_camera()