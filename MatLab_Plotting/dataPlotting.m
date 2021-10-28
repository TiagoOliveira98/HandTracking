MyFolderInfo = dir('../DataLogging/VelocityJoint/*.csv');

for file = 1:size(MyFolderInfo,1)

    fileName = MyFolderInfo(file).name;

    T = readtable(sprintf('../DataLogging/VelocityJoint/%s', fileName),'NumHeaderLines',1);
    
    Data = table2array(T);
    x = Data(:,1);
    Time = Data(:,43);

    %acc = zero

    sizeee = 0;
    t = 0;
    for i = 1:size(Data,1)
        t = 0;
        for a = 1:42
            if Data(i,a) ~= 0 
                t = t + 1;
            end
            %sizeee = sizeee + 1
        end
        if t > 0 
            sizeee = sizeee + 1;
        end
    end
    filterX = zeros(sizeee,1);
    filterT = zeros(sizeee,1);
    
    %figure(1)
    fh = figure('Name', sprintf('Right Hand Data from File %s', fileName),'NumberTitle','off')
    fh.WindowState = 'maximized';
    tiledlayout(4,6);
    
    for joint = 1:21
        x = Data(:,joint);
        it = 1;
        t = 0;
        filterX = zeros(sizeee,1);
        filterT = zeros(sizeee,1);
        

        for i = 1:size(x,1)
            %if x(i,1) ~= 0 
            %    filterX(it,1) = x(i,1);
            %    filterT(it,1) = Time(i,1);
            %    it = it +1;
            %end
            t = 0;
            for a = 1:42
                if Data(i,a) ~= 0 
                    t = t + 1;
                end
                %sizeee = sizeee + 1
            end
            if t > 0 
                filterX(it,1) = x(i,1);
                filterT(it,1) = Time(i,1);
                it = it +1;
            end
        end

        %test
        if joint == 1
            filterV = filterX;
        else
            filterV = [filterV filterX];
        end
        %
        %if joint == 1
        %    all = filterX;
        %else
        %    all = [all filterX]
        %end
      
        nexttile
        plot(filterT,filterX,'-o')
        ylim([0 100])
        title(sprintf('Plot of the variance of mean Velocity in the interval from last Timestamp represented to this one of the Joint%i of the Right Hand', joint-1))
        ylabel('Mean Velocity') 
        xlabel('Final Timestamp in Interval from last Timestamp represented to this one') 
        legend({'Mean Velocity'},'Location','northwest')
        ax = gca;
        ax.FontSize = 5;
    end
    
    %figure(2)
    fh = figure('Name', sprintf('Left Hand Data from File %s', fileName),'NumberTitle','off')
    fh.WindowState = 'maximized';
    tiledlayout(4,6);
    
    for joint = 22:42
        x = Data(:,joint);
        it = 1;
        for i = 1:size(x,1)
            if x(i,1) ~= 0 
                filterX(it,1) = x(i,1);
                filterT(it,1) = Time(i,1);
                it = it +1;
            end
        end
        
        filterV = [filterV filterX];

        nexttile;
        plot(filterT,filterX,'-o');
        ylim([0 100]);
        title(sprintf('Plot of the variance of mean Velocity in the interval from last Timestamp represented to this one of the Joint%i of the Left Hand', joint-22));
        ylabel('Mean Velocity') ;
        xlabel('Final Timestamp in Interval from last Timestamp represented to this one') ;
        legend({'Mean Velocity'},'Location','northwest');
        ax = gca;
        ax.FontSize = 5;
    end

    %Acceleration
       
    %acc = filterX.^2
    Freq = 1/0.02;
    acc = zeros(size(filterV,1)-1,42);
    for i = 1:size(filterV,1)-1
        for a = 1:42
            acc(i,a) = (filterV(i+1, a) - filterV(i,1)) / (filterT(i+1,1) - filterT(i,1));
        end
    end

    fh = figure('Name', sprintf('Right Hand Acceleration Data from File %s', fileName),'NumberTitle','off')
    fh.WindowState = 'maximized';
    tiledlayout(4,6);
    
    for joint = 1:21
            nexttile;
            plot(acc(:,joint),'-o');
            ylim([-2000 2000]);
            title(sprintf('Plot of the variance of Acceleration in the interval from last Timestamp represented to this one of the Joint%i of the Right Hand', joint-1));
            ylabel('Acceleration') ;
            xlabel('Final Timestamp in Interval from last Timestamp represented to this one') ;
            legend({'Accerelation'},'Location','northwest');
            ax = gca;
            ax.FontSize = 5;
    end
    
    fh = figure('Name', sprintf('Left Hand Acceleration Data from File %s', fileName),'NumberTitle','off')
    fh.WindowState = 'maximized';
    tiledlayout(4,6);
    
    for joint = 22:42
            nexttile;
            plot(acc(:,joint),'-o');
            ylim([-2000 2000]);
            title(sprintf('Plot of the variance of Acceleration in the interval from last Timestamp represented to this one of the Joint%i of the Left Hand', joint-22));
            ylabel('Acceleration') ;
            xlabel('Final Timestamp in Interval from last Timestamp represented to this one') ;
            legend({'Accerelation'},'Location','northwest');
            ax = gca;
            ax.FontSize = 5;
    end
end
