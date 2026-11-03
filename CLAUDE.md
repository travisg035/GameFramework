# UnityGameFramework (GameFramework Core) - Project Rules & Architecture Standards

Tài liệu này là hiến pháp kỹ thuật và bộ quy tắc thực hiện dự án dành cho **GameFramework** (lõi C# thuần của UnityGameFramework). Toàn bộ AI agent và nhà phát triển bắt buộc tuân thủ nghiêm ngặt. File này chỉ chứa quy chuẩn kỹ thuật và nguyên tắc thiết kế, không chứa nhật ký hay trạng thái tiến độ công việc.

---

## 1. Tổng quan Kiến trúc & Triết lý Thiết kế

### 1.1. Bản chất Dự án
- **Pure C# Core (Engine-Agnostic):** Thư viện C# thuần (`GameFramework.dll`), không tham chiếu trực tiếp đến `UnityEngine.dll`.
- **Helper & Dependency Inversion Pattern:** Mọi tương tác phần cứng, hệ điều hành, runtime hoặc Unity Engine đều trừu tượng hóa qua các interface Helper (`IResourceHelper`, `ISoundHelper`, `IUIFormHelper`, `ITextHelper`, `ILogHelper`, `IDownloadAgentHelper`, `INetworkChannelHelper`). UnityGameFramework phía Unity Engine chịu trách nhiệm hiện thực hóa (implement).
- **Service Locator & Module Manager:** `GameFrameworkEntry` là điểm truy cập trung tâm, quản lý vòng đời (`Update`, `Shutdown`) và khởi tạo các module kế thừa `GameFrameworkModule` theo độ ưu tiên (`Priority`).

### 1.2. Danh mục 21 Module Cốt lõi
1. **Base:** `GameFrameworkEntry`, `ReferencePool`, `EventPool`, `TaskPool`, `DataProvider`, `GameFrameworkLinkedList`.
2. **Config:** Quản lý cấu hình toàn cục chỉ đọc dạng Key-Value.
3. **DataNode:** Quản lý cây dữ liệu phân cấp runtime.
4. **DataTable:** Quản lý bảng dữ liệu cấu hình game (parse nhị phân / text tùy biến).
5. **Debugger:** Khung cửa sổ hiển thị thông tin debug runtime.
6. **Download:** Quản lý tải tệp đa luồng, hỗ trợ tiếp tục phân đoạn (resume).
7. **Entity:** Quản lý thực thể logic/hiển thị trong thế giới game, nhóm thực thể, gắn kết thực thể.
8. **Event:** Quản lý sự kiện toàn cục theo cơ chế Observer, giải phóng phụ thuộc logic.
9. **FileSystem:** Hệ thống tệp ảo đóng gói tài nguyên, đọc dữ liệu phân đoạn tối ưu I/O.
10. **Fsm:** Máy trạng thái hữu hạn cho logic đối tượng và gameplay (hỗ trợ `RollbackState` về trạng thái trước).
11. **Localization:** Quản lý đa ngôn ngữ (chuỗi văn bản và tài nguyên bản địa hóa).
12. **Network:** Quản lý kết nối mạng Socket đa giao thức (TCP, UDP Unreliable Datagram, KCP Reliable UDP ARQ), heartbeat, packet framing.
13. **ObjectPool:** Bể đối tượng cấp cao với cơ chế kiểm soát tuổi thọ (expire), dung lượng, tự động giải phóng.
14. **Procedure:** Máy trạng thái quy trình vòng đời toàn bộ game (dựa trên FSM).
15. **Resource:** Quản lý tài nguyên bất đồng bộ, nạp Asset/Scene/Binary, quản lý VersionList và cập nhật online.
16. **Scene:** Quản lý nạp và dỡ bỏ scene phân đoạn (Additive loading).
17. **Setting:** Quản lý lưu trữ cài đặt người chơi (PlayerPrefs / Custom storage).
18. **Sound:** Quản lý âm thanh, nhóm âm thanh, hiệu ứng 2D/3D và gắn vị trí entity.
19. **UI:** Quản lý giao diện người dùng, nhóm UI, độ sâu (depth), mở/đóng và tái chế form.
20. **WebRequest:** Quản lý gọi HTTP ngắn hạn (GET/POST), quản lý tác vụ đồng thời.
21. **Timer:** Quản lý bộ đếm thời gian, hẹn giờ logic (Scaled/Unscaled, Loop, Pause/Resume, Cancel) thuần C# độc lập engine.

### 1.3. Triết lý Thiết kế Cốt lõi
- **Zero-GC Allocation Obsession:** Không cấp phát bộ nhớ động trên Heap trong vòng lặp runtime (`Update`). Mọi đối tượng tạm thời phải tái chế qua `ReferencePool` hoặc `ObjectPool`.
- **Pure Asynchronous Resource Loading:** Không hỗ trợ tải đồng bộ gây chặn main thread làm giật khung hình.
- **Strict Decoupling:** Các module hoạt động độc lập cao, chỉ giao tiếp qua `GameFrameworkEntry.GetModule<T>()` hoặc bắn sự kiện qua `EventManager`.

---

## 2. Nguyên tắc Bắt buộc (Non-Negotiable Rules)

1. **Bảo tồn Kiến trúc Gốc (Preserve Original Architecture):**
   - Không thay đổi cơ chế Service Locator (`GameFrameworkEntry`), mô hình Module (`GameFrameworkModule`), hệ thống `TaskPool`, `EventPool`, `ReferencePool`, và các Helper Interface.
   - Không biến thư viện C# thuần thành thư viện phụ thuộc Unity Engine.
2. **Tương thích Ngược Tuyệt đối (Backward Compatibility):**
   - Không xóa, đổi tên hoặc thay đổi chữ ký (signature) cũng như hành vi của các `public` API hiện có.
   - Tính năng và API mới phải là dạng bổ sung (additive) qua Overload hoặc Extension Method.
3. **Cải tiến Từng bước (Incremental Improvement):**
   - Nâng cấp theo từng bước nhỏ, kiểm thử độc lập. Không rewrite quy mô lớn.
   - Mọi thay đổi cấu trúc dữ liệu phải đảm bảo tính tương thích với định dạng nhị phân cũ (`FileSystem`, `VersionList`).
4. **Hiệu năng & Zero-GC (Performance First):**
   - Cấm dùng Linq, lambda capturing variables, auto-boxing struct, params object[] trong runtime path.
   - Xử lý mảng/chuỗi/buffer bắt buộc ưu tiên `Span<T>`, `ReadOnlySpan<T>`, `Memory<T>`, và `ArrayPool<T>`.
5. **Tương thích Modern .NET, Unity IL2CPP & AOT:**
   - Mọi tính năng C# áp dụng phải tương thích Unity IL2CPP, Mono, và cơ chế AOT / Code Stripping.
   - Cấm Reflection nặng trong runtime path; cấm dynamic code generation (`Reflection.Emit`).
6. **Giữ đúng Phạm vi Dự án (Keep Original Purpose):**
   - Duy trì mục đích framework nền tảng cho game client/server. Cấm đưa các hệ thống ngoài phạm vi (AI phức tạp, ECS, Rendering) vào lõi framework.
7. **Phụ thuộc Tối thiểu (Minimal Dependencies):**
   - Không thêm package NuGet bên ngoài trừ các package chuẩn từ Microsoft/.NET Foundation và Unity (`System.Memory`, `System.Buffers`).
8. **Phong cách & Chuẩn Code (Code Quality & Style):**
   - Giữ nguyên coding conventions gốc: PascalCase cho public/protected, `m_` cho private field, `s_` cho static field, XML documentation đầy đủ bằng tiếng Anh.
9. **Quy tắc Header Bản quyền cho File Mới (New File Attribution):**
   - **Chỉ áp dụng cho file mới tạo** (giữ nguyên header của mọi file cũ đã tồn tại).
   - File mới bắt buộc dùng header bản quyền KhắcDuy:
   ```csharp
   //------------------------------------------------------------
   // Game Framework
   // Copyright © KhắcDuy. All rights reserved.
   // GitHub: https://github.com/Travisg035 / https://github.com/Travisg034
   //------------------------------------------------------------
   ```

---

## 3. Quy chuẩn Kỹ thuật Chi tiết

### 3.1. Target Framework & C# Compiler Directives
- **Target Frameworks:** Multi-targeting `netstandard2.0;netstandard2.1;net8.0`.
- **C# Language:** `LangVersion: latest`.
- **Conditional Compilation:** Dùng `#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER` để kích hoạt API hiện đại, luôn cung cấp fallback đầy đủ cho `netstandard2.0`.
- **Delegates:** Giữ nguyên chữ ký các delegate nền tảng (`GameFrameworkAction`, `GameFrameworkFunc`) để bảo đảm tương thích ngược 100%.

### 3.2. Quản lý Bộ nhớ, Buffer & Zero-GC
- **Span & Memory Usage:**
  - Các hàm tiện ích chuyển đổi, mã hóa, nén, kiểm tra tính toàn vẹn (CRC32) và xử lý packet framing phải cung cấp overload nhận `ReadOnlySpan<byte>` và `Span<byte>`.
  - Buffer tạm: dùng `stackalloc byte[]` cho kích thước nhỏ (< 256 bytes) hoặc mượn từ `ArrayPool<byte>.Shared` cho kích thước lớn; bắt buộc hoàn trả buffer trong khối `finally`.
- **Xử lý Chuỗi & Đường dẫn:**
  - Chuẩn hóa đường dẫn và cắt ghép chuỗi tối ưu qua `ReadOnlySpan<char>`, tránh cấp phát chuỗi trung gian.
  - Định dạng chuỗi tận dụng `Span`-based formatting hoặc `ISpanFormattable` trên môi trường hỗ trợ.
- **Streams & Serialization:**
  - Các serializer (`GameFrameworkSerializer`, `PackageVersionListSerializer`, `FileSystem`) tránh tạo mới `MemoryStream` liên tục; ưu tiên tái sử dụng buffer hoặc pool.

### 3.3. Chuẩn Bất đồng bộ (Async Facade Pattern)
- **Giữ nguyên Callback ở Tầng Core:**
  - Toàn bộ core nội bộ (`TaskPool`, `ResourceManager`, `DownloadManager`, `WebRequestManager`) giữ nguyên cơ chế callback để phục vụ pooling, cấm sinh `Task` allocation ngầm bên trong core.
- **Cung cấp Async qua Extension Methods:**
  - Mọi API bất đồng bộ hiện đại (`Task<T>`, `ValueTask<T>`) chỉ được cung cấp qua Extension Method ở tầng ngoài (`<Module>Extension`).
  - Extension method phải tự quản lý việc liên kết Callback/Event sang `TaskCompletionSource<T>`, bắt buộc recycle event args và hủy đăng ký callback khi hoàn thành hoặc lỗi.

### 3.4. Tương thích IL2CPP, Mono & Code Stripping
- **Module Discovery & Registration:**
  - Duy trì cơ chế tự động tìm kiếm module qua Reflection, đồng thời bắt buộc cung cấp cơ chế đăng ký tường minh (Explicit Registration) qua `GameFrameworkEntry.RegisterModule<T>()` để chống code stripping trên IL2CPP.
- **Generic Constraints & AOT:**
  - Mọi kiểu dữ liệu dùng trong `ReferencePool.Acquire<T>()` bắt buộc có ràng buộc `where T : class, IReference, new()`.
  - Tránh lồng các kiểu generic ảo sâu không giới hạn gây lỗi AOT compiler trên iOS/Console.

### 3.5. Quy chuẩn Kênh Mạng (Network Channels)
- **KCP Channel (`ServiceType.Kcp`):**
  - Giao thức ARQ Reliable UDP thuần C#.
  - Quản lý phân đoạn: Tái chế 100% `KcpSegment` qua `ReferencePool`.
  - Cấu hình qua `IKcpChannelHelper`: MTU, Fast Resend, NoDelay, Send/Receive Window, Congestion Control.
  - Phạm vi áp dụng: Dữ liệu gameplay quan trọng cần độ trễ thấp và độ tin cậy cao (RPC, combat events, skill, inventory).
- **UDP Channel (`ServiceType.Udp`):**
  - Giao thức Unreliable Datagram trên raw socket.
  - Tối ưu throughput: Flush toàn bộ queue packet trực tiếp vào socket buffer mỗi tick, không dùng hàng đợi khóa phức tạp.
  - Phạm vi áp dụng: Dữ liệu streaming tần số cao chấp nhận rơi rớt (vị trí/vận tốc, transform sync, voice, ping).
- **TCP Channel (`ServiceType.Tcp` / `ServiceType.TcpWithSyncReceive`):**
  - Kết nối stream tin cậy truyền thống.

---

## 4. Quy trình Thực hiện & Đánh giá Thay đổi

Mọi thay đổi mã nguồn bắt buộc tuân thủ quy trình 5 bước:

1. **Phân tích Hiện trạng:** Đọc và hiểu toàn bộ luồng code liên quan, xác định các trường hợp biên (edge cases).
2. **Đánh giá Cấp phát & Hiệu năng:** Xác định chính xác vị trí phát sinh GC Alloc, boxing hoặc lãng phí CPU cycles.
3. **Thiết kế Giải pháp Tối giản:** Native/stdlib trước tiên, tôn trọng nguyên tắc `ponytail:` (đơn giản, dễ bảo trì, có đường nâng cấp).
4. **Bảo toàn Tương thích API:** Đảm bảo code cũ gọi vào API vẫn biên dịch và hoạt động 100% không cần chỉnh sửa.
5. **Tự Kiểm tra (Self-Check):** Chạy build và viết kiểm thử tự kiểm tra (assert-based check hoặc unit test) xác minh tính đúng đắn trước khi hoàn tất.

Mẫu đề xuất thay đổi chuẩn:
- **Vấn đề Hiện tại:** File, dòng, hành vi gây chậm hoặc cấp phát heap.
- **Mục tiêu Kỹ thuật:** Lý do cần thay đổi.
- **Giải pháp Đề xuất:** Diff tối thiểu, mã nguồn minh họa.
- **Đánh giá Tương thích:** Tương thích ngược API, IL2CPP/Mono, định dạng nhị phân.
- **Đo lường Hiệu năng:** Giảm GC Alloc, giảm copy buffer, giảm CPU overhead.

---

## 5. Quy tắc Vận hành Dành riêng cho Claude Code & AI Agents

- **Khảo sát Trước khi Sửa:** Luôn dùng `Read` hoặc `Grep` kiểm tra toàn bộ dependency và cấu trúc xung quanh file trước khi chỉnh sửa.
- **Không Tự ý Thêm Thư viện:** Tuyệt đối không thêm package NuGet bên ngoài mà không có yêu cầu rõ ràng từ người dùng.
- **Giải trình Hiệu năng Dựa trên Cơ chế CLR:** Mọi tối ưu hóa phải giải thích rõ ràng dựa trên cơ chế cấp phát bộ nhớ và quản lý tài nguyên của CLR/.NET runtime.
- **Đồng nhất Phong cách:** Giữ nguyên quy chuẩn thụt dòng (tabs/spaces), đặt tên biến, comment của file đang thao tác.
- **Bản quyền File Mới:** Áp dụng chính xác header bản quyền KhắcDuy cho mọi file mới tạo theo Mục 2.9. Không sửa header của các file hiện hữu.
- **Xác thực Biên dịch:** Kiểm tra lại mã nguồn bằng `dotnet build` để bảo đảm không gây lỗi biên dịch sau khi sửa.
